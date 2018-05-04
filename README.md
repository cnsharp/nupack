# NuPack
Visual Studio extension for building and publishing NuGet packages.

### Features
* Build and Deploy Packages

![Deploy command menu](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/DeployCommandMenu.png)
![Package metadata](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/PackageMetadataDialog.png)
![Package metadata](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/DeployDialog.png)

* Classic projects support

	* Add packages.nuspec
![Add packages.nuspec](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/NuspecCommandMenu.png)

	 * Common Assembly Info Editor

![Assembly info command menu](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/AssemblyInfoCommandMenu.png)
![Assembly info editor](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/AssemblyInfoDialog.png)


* SDK-based Projects Support

	* Add Directory.Build.props to manage common package info for SDK-based projects.

![Add Directory.Build.props](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/2.x/DirectoryBuildPropsCommandMenu.png)

* Auto Increment Version
	* For classic projects,use AssemblyInfo with wildcard version number like '1.0.*'.
	* For SDK-based projects,you can input version number contains wildcard in the dialog and then it will be replaced by this extension during build.

### Release notes

[Release notes](https://raw.githubusercontent.com/cnsharp/nupack/master/src/release_notes.txt)

### Download

[Visual Studio Gallery](https://marketplace.visualstudio.com/items?itemName=CnSharpStudio.NuPack)

### Fork on Github
[https://github.com/cnsharp/nupack](https://github.com/cnsharp/nupack)
 