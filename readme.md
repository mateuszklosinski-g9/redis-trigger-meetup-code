# Custom Trigger (based on Redis channels)

This repository contains:
- A custom trigger for Azure Function that listens to Redis channels (for both Isolated Worker and In-Process models)
- Subscriber function (Isolated worker)
- Subscriber debugger function (in-process)
- Script for publishing nuget packages on Azure DevOps

## Getting Started

1. **Clone the repository:**
2. **Open the solution in Visual Studio.**
3. **Build the solution** using the Build menu or `Ctrl+Shift+B`.
4. **Create NuGet feed** in Azure DevOps:
   - Go to your Azure DevOps project.
   - Navigate to "Artifacts" and create a new feed.
   - Set the feed visibility and permissions as needed.
5. Set feed details in `azure-pipelines.yml`
6. Create publish pipeline on Azure DevOps
   - Go to "Pipelines" and create a new pipeline.
   - Select "Azure Repos Git" and choose your repository.
   - Select "Existing Azure Pipelines YAML file" and choose the `azure-pipelines.yml` file in the root of the repository.
   - Save and run the pipeline to publish the NuGet package to your feed.
7. Configure global Nuget.Config file and set the feed credentials.
8. Set the future package version in `RedisTriggerAttribute.cs` file using ExtensionInformation attribute for `G9.Redis.Trigger.Isolated` project.
8. Run the pipeline and publish the package.
4. **Run the application** using the Start button or `F5`.

## Requirements

- Visual Studio 2022 (or later)
- .NET 8 SDK and .NET 9 SDK

## Project Structure

- `src/` - Source code
- `readme.md` - Project documentation