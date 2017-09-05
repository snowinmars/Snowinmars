# Snowinmars
Stuff for my usual life

# How it works
Generally:
* Sass rebuilds as pre build action.
* Deploy with Deployer.exe

What doesn't work:
* all workers: I want to refactor it.
* import button on Path of Exile page
* editing book changes owned to current

User:
* Not authorized user can see lists of books and authors and Path of Exile page. Not authorized can register.
* Banned user redirects from anyway to ban page.
* "User" user can see all that "not authorized" user can see plus books wishlist and personal page
* Admin user is "user" user plus admin can manipulate "user" users data without limitations.
* Root user is admin user plus root can promote or demote users' status. Root can access root page to set up workers.

Books - my personal library.
* Anyone can see books.
* Only authorized users can add books. Added book is a property of user.
* Authorized users can edit and deleting their own books.
* Admin and root can add, edit and remove every book. If admin or root added book, he is the owner, if admin or root changed book - owner should not be changed.
* Book can have status "Have" (you have it) or "Wished" (you want to buy it).

Path of Exile - best trade calculator.
* Export and import format is number-space-number ("x y z")
* user can't calculate more than 20 values