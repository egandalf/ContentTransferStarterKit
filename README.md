# Ektron to EPiServer Content Transfer Starter Kit

This is a starter kit, provided without warranty, to aide in the transfer of content, blobs/assets, and structure from an instance of Ektron to an instance of EPiServer DXC.

The intent of this project is to provide development teams with functional sample code that covers some of the basic challenges in not just transporting content from Ektron to EPiServer, but also the transform from Ektron models to EPiServer models, including nested models that are common in Ektron Smart Forms.

## Restoration of the Solution

To restore the EktronAPI project, you will need an installation of Ektron CMS version 8.5 or higher as this project depends on WCF endpoints only available with the fully released Framework API (not the preview release available in Ektron 8.02). This code was tested against Ektron 9.1SP1. If you are running an older version of Ektron, you may want to consider upgrading it to 8.5 or later prior to transporting content to EPiServer in order to make use of these APIs.

Restoration is simple:

* Download the project ZIP file and open the solution (.sln) in Visual Studio.
* Restore Nuget packages
* Add References to the EktronAPI project
  * Choose Browse...
  * Navigate to [Ektron Install Folder]\[Version]\startersites\3TierMin\Content\bin
  * Select all files

Adding the references may throw a warning if one of the references happens to already exist, but this should not interrupt adding the other references.

## Configuration

There are currently three console application projects within this solution. Each is prefixed with "App" to distinguish them from the other projects.

* **AppCopyFolderContent** - transfers select folders of Ektron content to a destignated location
* **AppTransferAssetTree** - transfers image assets from Ektron while also copying the folder structure in which they're stored
* **AppTransferContentTree** - transfers the Site IA using an Ektron Menu as the source

One which transfers the Site IA, one which transfers select folders of Ektron content, and one which transfers Image assets. Each of these contains an App.config file that will have to be modified in order to successfully connect to both Ektron and EPiServer.

The following two sections control the connectivity between Ektron and EPiServer DXC:

```xml
<EktronCredentials Username="RemoteUser" Password="RemotePass" BaseUrl="http://staging.ektronsite.com/" />
<EPiServerCredentials ServiceUrl="https://www.episerversite.com" Username="RemoteUser" Password="RemotePass" ImportRoot="3742" />
```

In the respective sections, update the URL, Username, and Password values as appropriate for your environment. The Ektron user should have a minimum of read access to all content to be retrieved. If you plan to retrieve Archived or Expired content, the user will need elevated permissions, such as Administrator access, in order to retrieve such protected information. The EPiServer user should have access to create content (Page and Block types) in all relevant site sections and of all relevant types as well as create folders and upload images to those folders.

Other configuration information within App.config is important primarily for managing connectivity to Ektron. You also should update the ServicesPath value to point at the workarea/services/ directory of your Ektron installation. This directory is restricted to IP '127.0.0.1' by default. If you're running the console application(s) remotely to your Ektron instance, access permissions will have to be granted to your IP address through settings in IIS.

```xml
<add key="ek_ServicesPath" value="http://staging.ektronsite.com/workarea/services/" />
```

## The Content Transfer Process

Ektron uses a Folder structure to store content while EPiServer DXC relies on a Content Hierarchy, generally matching the site IA. The implication being that there is no direct translation from Ektron content storage to EPiServer DXC content storage. Instead, first look for an architecure within Ektron that closely matches the type of structure found in EPiServer. In the vast majority of cases, this will be an Ektron Menu.

The first app to run in this sample is therefore **AppTransferContentTree** in order to set the stage with a solid Content Hierarchy. Note that unrecognized content types are added to EPiServer DXC as 'Container' types.

Set the values for your source and destination by modifying `Program.cs` within the application project.

```c#
static void Main(string[] args)
{
    var ekapi = new Business.Ektron.EkContentAPI();
    var ekmenuapi = new Business.Ektron.EkMenuAPI();
    var epapi = new Business.EPiServer.EPiContentAPI();
    Console.WriteLine("Start!");
    // Menu ID to be transferred
    var menu = ekmenuapi.GetMenuTree((long)6); // <---!!!
    // Second parameter is the EPiServer destination reference.
    ProcessMenuItems(menu, "3460"); // <---!!!
    Console.WriteLine("Done!");
    Console.ReadLine();
}
```

With a starting point in place, run **AppCopyFolderContent** to bulk move items in folders to appropriate locations in the content tree. For example, the source Menu in Ektron may contain a link for *Press Releases* but will not contain each and every Press Release. Log into EPiServer CMS and find the ID/Reference for the Press Releases node in the Content Hierarchy and use that as your destination. Then use the ID of the Ektron Folder containing your Press Release content to copy all of that content over to the new environment.

Again, set values within `Program.cs`:

```c#
static void Main(string[] args)
{
    Console.WriteLine("Loading Ektron content... ");
    Console.WriteLine("(This may take a few minutes, depending on amount of data to return.)");
    // The first parameter is the Ektron folder ID.
    var blogs = ekAPI.GetContentList<GenericContent>(125, Common.Enumeration.ContentSourceType.Folder, LanguageId); // <---!!!
    Console.WriteLine(blogs.Count() + " content items found.");
    Console.WriteLine("Loading content into EPiServer DXC.");
    string cref;
    foreach (var blog in blogs)
    {
        // Sets a parent reference to each blog post being added.
        blog.ParentContentReference = "5915"; // <---!!!
        cref = epAPI.CreateContent<GenericContent>(blog);
        Console.WriteLine("Content Transferred.");
        Console.WriteLine("Ektron ID: " + blog.SourceId + "\tEPiServer ID: " + cref);
    }
    Console.WriteLine("Done!");
    Console.ReadLine();
}
```

The last console app, **AppTransferAssetTree**, does not have a dependency on a content hierarcy. It will create a new folder structure under the Media section of EPiServer DXC and add image assets from Ektron to the CMS. The folder structure created will mimic that found in Ektron in order to avoid "dumping" images into a single directory.

Note that it would be ideal to transfer images used by content as the content itself is added to EPiServer DXC.

Again, source and destination values are configured within `Program.cs`, similar to the above snippets.

##Limitations, aka Improvement Opportunities

1. *Only `List<>` types are supported for deserialization*, not Arrays or other collections
2. The code does not attempt to update or correct anything within the content, including *cross-links and images*.
3. *Redirects (301 or 302)* and 404 error pages are not handled.
4. Code does not currently support *multiple passes* - it will create duplicates.
5. *Taxonomy* is not currently addressed by this code.
6. *Users and User-Generated Content* are not currently addressed by this code.
7. *Assets/Blobs* may only be uploaded if under the request limit of the EPiServer instance - the code does not currently support chunking for upload.
8. *Content/Folder References* via Smart Form or Metadata are currently ignored.
