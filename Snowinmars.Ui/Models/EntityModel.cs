using System;
using System.ComponentModel.DataAnnotations;
using Snowinmars.Ui.App_LocalResources;

namespace Snowinmars.Ui.Models
{
    public abstract class EntityModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Model_IsSynchronized", ResourceType = typeof(Global))]
        public bool IsSynchronized { get; set; }
    }
}