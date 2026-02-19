# Developer Notes

This is just some small notes that help with the development and support of this application.

## Branches

The *Main* Branch will contain the current release code.  Changes will be in the *develop* branch.  
For new Changes create a branch from the *develop* branch and make your changes.  Then Create a pull request
to merge into *develop*.  Once all the changes are ready for release, create a pull request to merge 
*develop* into *main*.  Once the release has been merged into main.  Create a new Branch from Main called
*release/vX.X.X* as a backup for the production release.

## GitHub Pages

Currently this is using the [leapday theme](https://github.com/pages-themes/leap-day)  
So any updates that is needed will need to be taken from that github repo.

### List of Customized items

Below is a list of customized items for the leapday theme that I modified for my use.
Things that will have to be backedup or updated if a newer lead day theme was used.
All located in the docs folder

* _includes/main_menu.html
* _layouts/default.html



## Things to Do Before Release

* Update the Change Log with Release Version and any additional information
* Update the Online Help with Any new Pages that are needed
* Build the Setup MSI Package
* Create the Release On Github with Change Log Details
* Update the Github Pages Main README with the Change Log Information.
* 

## Backup and Restore Configs

Since the Backup and Restore application where replaced with nuget packages, it is best that we store the config to update 
accordinly when the package is updated

### Backup Application Config

```xml
<appSettings>
    <add key="AppName" value="DBBackup"/>
    <add key="MainAppName" value="My Loaders Log"/>
    <add key="DBName" value="MLL.mdb"/>
    <add key="RegKey" value="Software\BurnSoft\BSMLL\"/>
    <add key="CheckProcess" value="false"/>
    <add key="LogFilename" value="dbbackup.err.log"/>
    <add key="AppABV" value="MLL"/>
</appSettings>

```


### Retore Application Config

```xml
<appSettings>
    <add key="AppName" value="DBRestore"/>
    <add key="MainAppName" value="My Loaders Log"/>
    <add key="MainAppNameEXE" value="BSMyLoadersLog.exe"/>
    <add key="DBName" value="MLL.mdb"/>
    <add key="RegKey" value="Software\BurnSoft\BSMLL\"/>
    <add key="CheckProcess" value="false"/>
    <add key="LogFilename" value="dbrestore.err.log"/>
    <add key="AppABV" value="MLL"/>
</appSettings>

```