using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.AspNetCore.Components.Authorization;

namespace PWA.Components
{
    public partial class FileUploadComponent : ComponentBase
    {
        [Parameter]
        public string FileTypes { get; set; }

        [Parameter]
        public int MaxFiles { get; set; } = 10;

        [Parameter]
        public int MaxSize { get; set; } = 1024 * 5;

        [Parameter]
        public EventCallback<bool> CanSelect { get; set; }

        [Inject]
        AuthenticationStateProvider GetAuthStateProvider { get; set; }

        MudMessageBox? msgBoxCapacityError, msgBoxDuplicateWarning;
        List<CustomBrowserFile> Files = new();
        List<IBrowserFile> Duplicates = new();
        string path, username;
        int mainProgress = 0, filesUploaded = 0, fileCount;
        bool isLoading;

        protected override async Task OnInitializedAsync()
        {
            var authstate = await GetAuthStateProvider.GetAuthenticationStateAsync();
            username = authstate?.User?.Identity.Name;
        }

        void OnInputFileChange(InputFileChangeEventArgs e)
        {
            isLoading = true;

            try
            {
                fileCount = e.FileCount;
                var browserFiles = e.GetMultipleFiles(MaxFiles);

                foreach (var file in browserFiles)
                {
                    var customBrowserFile = new CustomBrowserFile() { File = file };

                    if (file.Size > MaxSize)
                    {
                        customBrowserFile.Status = FileStatus.Error;
                        customBrowserFile.Message = "This file is too large";
                    }
                    else if (!FileTypes.Contains(Path.GetExtension(file.Name)))
                    {
                        customBrowserFile.Status = FileStatus.Error;
                        customBrowserFile.Message = $"This file has the wrong extension. Only the files of type(s) {FileTypes} are allowed.";
                    }
                    else
                    {
                        customBrowserFile.Status = FileStatus.Loaded;
                        customBrowserFile.Message = "File selected";
                    }

                    Files.Add(customBrowserFile);
                }
            }
            catch (Exception)
            {
                msgBoxCapacityError?.Show();
                return;
            }

            if (Duplicates.Count > 0)
                msgBoxDuplicateWarning?.Show();

            isLoading = false;
            StateHasChanged();
        }

        async Task UploadOne(CustomBrowserFile file)
        {
            file.Status = FileStatus.Uploading;
            await InvokeAsync(StateHasChanged);

            try
            {
                await using FileStream fs = new(Path.Combine(path, file.File.Name), FileMode.Create);
                await file.File.OpenReadStream(MaxSize).CopyToAsync(fs);

                file.Status = FileStatus.Uploaded;
                file.Message = "File uploaded";

                filesUploaded++;
                mainProgress = (filesUploaded / fileCount) * 10;

                await CanSelect.InvokeAsync(true);
            }
            catch (Exception ex)
            {
                file.Status = FileStatus.Error;
                file.Message = ex.Message;
            }

            await InvokeAsync(StateHasChanged);
        }

        async Task UploadAll()
        {
            foreach (var file in Files)
            {
                if (file.Status != FileStatus.Error)
                    await UploadOne(file);
            }
        }

        public string[] GetCurrentFiles() => Files.Where(f => f.Status == FileStatus.Uploaded).Select(f => f.File.Name).ToArray();

        void RemoveOne(CustomBrowserFile file)
        {
            Files.Remove(file);
            StateHasChanged();
        }

        void RemoveAll()
        {
            Files.Clear();
            StateHasChanged();
        }

        void DeleteOne(CustomBrowserFile file)
        {
            File.Delete(Path.Combine(path, file.File.Name));
            RemoveOne(file);
            StateHasChanged();
        }

        Color GetColor(FileStatus status)
        {
            switch (status)
            {
                case FileStatus.Loaded: return Color.Info;
                case FileStatus.Uploading: return Color.Transparent;
                case FileStatus.Uploaded: return Color.Success;
                default: return Color.Error;
            }
        }

        string GetSizeText(long size)
        {
            string[] sizeLabels = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (size >= 1024 && order < sizeLabels.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return String.Format("{0:0.##} {1}", size, sizeLabels[order]);
        }

        bool IsSelectDisabled() => Files.Count >= MaxFiles;
        bool isUploadDisabled(CustomBrowserFile file) => file.Status == FileStatus.Error;
        bool ShowUploadAll() => Files.Count < 0;
    }
}
