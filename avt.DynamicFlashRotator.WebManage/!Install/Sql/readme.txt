SQL Setup for Runtime Administration
--------------------------------------------------------------------------

In this folder you'll find sql scripts that will install the database tables required by Dynamic Rotator to save data.

Before executing the SQL scripts, make sure to replace the following:
    {databaseOwner} - with the actual database owner you're using
    {objectQualifier} - if you want to use a prefix for the tables replace this with that prefix. Otherwise replace with nothing.
        Note that if you do set an objectQualifier you must also provide it as an attribute to the Dynamic Rotator .NET server tag


New Installs
--------------------------------------------------------------------------

If this is a new install, just run scripts in !NewInstall.sql and it will install eveything.


Upgrades
--------------------------------------------------------------------------

There are separate installation scripts for each version. 
In case you're upgrading from older version you only need to run to newer scripts.


Uninstall
--------------------------------------------------------------------------    

Uninstall.sql file contains queries for dropping the tabales and their foreign keys.