namespace Snowinmars.Dao
{
	using System;
	using System.Data;
	using System.Data.SqlClient;

	public class DatabaseCommand
	{
		private readonly SqlCommand sqlCommand;
		private int? sqlCommandTimeout;

		public DatabaseCommand(CommandType commandType, string command)
		{
			this.sqlCommand = new SqlCommand()
			{
				CommandText = command,
				CommandType = commandType
			};
		}

		public int CommandTimeout
		{
			get => this.sqlCommandTimeout ?? this.sqlCommand.CommandTimeout;
			set => this.sqlCommandTimeout = value;
		}

		public static DatabaseCommand StoredProcedure(string spName)
		{
			return new DatabaseCommand(CommandType.StoredProcedure, spName);
		}

		public SqlParameter AddInputOutputParameter(string parameterName, SqlDbType parameterType, object parameterValue)
		{
			var sqlParameter = this.CreateParameter(parameterName, parameterType, parameterValue, ParameterDirection.InputOutput);

			return this.AddParameter(sqlParameter);
		}

		public SqlParameter AddInputParameter(string parameterName, SqlDbType parameterType, object parameterValue)
		{
			var sqlParameter = this.CreateParameter(parameterName, parameterType, parameterValue, ParameterDirection.Input);

			return this.AddParameter(sqlParameter);
		}

		public SqlParameter AddInputTableParameter(string parameterName, DataTable parameterValue)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException("DataTable can't be null");
			}

			var sqlParameter = this.CreateParameter(parameterName, SqlDbType.Structured, parameterValue, ParameterDirection.Input);

			return this.AddParameter(sqlParameter);
		}

		public SqlParameter AddOutputParameter(string parameterName, SqlDbType parameterType)
		{
			var outputParameter = this.CreateOutputParameter(parameterName, parameterType);

			return this.AddParameter(outputParameter);
		}

		public SqlCommand GetSqlCommand(SqlConnection connection)
		{
			this.sqlCommand.Connection = connection;
			this.sqlCommand.CommandTimeout = this.sqlCommandTimeout ?? connection.ConnectionTimeout;

			return this.sqlCommand;
		}

		private SqlParameter AddParameter(SqlParameter parameter)
		{
			this.sqlCommand.Parameters.Add(parameter);

			return parameter;
		}

		private SqlParameter CreateOutputParameter(string parameterName, SqlDbType parameterType)
		{
			return new SqlParameter()
			{
				ParameterName = parameterName,
				SqlDbType = parameterType,
				Direction = ParameterDirection.Output,
			};
		}

		private SqlParameter CreateParameter(string parameterName, SqlDbType parameterType, object parameterValue, ParameterDirection parameterDirection)
		{
			SqlParameter parameter = new SqlParameter
			{
				ParameterName = parameterName,
				SqlDbType = parameterType,
				Direction = parameterDirection,
				Value = (this.IsEmptyParameter(parameterType, parameterValue) ? DBNull.Value : parameterValue),
			};

			return parameter;
		}

		private bool IsEmptyParameter(SqlDbType type, object value)
		{
			if (value == null)
			{
				return true;
			}

			return value.Equals(string.Empty) &&

				   (type == SqlDbType.NText ||
				   type == SqlDbType.NVarChar ||
				   type == SqlDbType.Text ||
				   type == SqlDbType.VarChar);
		}
	}
}