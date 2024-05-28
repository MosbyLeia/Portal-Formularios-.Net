@attribute [Authorize]
@inject IJSRuntime jsRuntime;
@inject IToastService toastService;
@using ProyectoPortalTramitesServer.Models;
@using ProyectoPortalTramitesServer.Helpers;
@using System.Collections.Generic;
@using System.Threading;
@using System.Threading.Tasks;
@using System.Globalization;
@inject IloggerManager logger;

<EditForm Model="formModel">
    <div class="row">
        <div class="row col-lg-6 mt-3 mb-3">
            <h5>Horario de mañana</h5>
            <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                <label for="manianaDesde">Desde </label>
                <select id="manianaDesde" class="form-select" bind="formModel.manianaDesde" @onchange="onChangeManianaDesde">
                    <option disabled selected="@string.IsNullOrEmpty(formModel.manianaDesde)" id="MD0000">--Seleccionar--</option>
                    <option id="MD0700">07:00</option>
                    <option id="MD0730">07:30</option>
                    <option id="MD0800">08:00</option>
                    <option id="MD0830">08:30</option>
                    <option id="MD0900">09:00</option>
                    <option id="MD0930">09:30</option>
                    <option id="MD1000">10:00</option>
                    <option id="MD1030">10:30</option>
                    <option id="MD1100">11:00</option>
                    <option id="MD1130">11:30</option>
                </select>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                @if (!String.IsNullOrEmpty(formModel.manianaDesde))
                {
                    <label for="manianaHasta">Hasta </label>
                    <select id="manianaHasta" class="form-select" value="@formModel.manianaHasta" @onchange="onChangeManianaHasta">
                        @foreach (var horario in morningEndOptions)
                        {
                            <option id=@horario.Replace(":","") value="@horario" selected="@(formModel.manianaHasta == horario)">@horario</option>
                        }
                    </select>
                }
            </div>
        </div>

        <div class="row col-lg-6 mt-3 mb-3">
            <h5>Horario de tarde</h5>
            <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                <label for="tardeDesde">Desde </label>
                <select id="tardeDesde" class="form-select" bind="formModel.tardeDesde" @onchange="onChangeTardeDesde">
                    <option disabled selected="@string.IsNullOrEmpty(formModel.tardeDesde)" id="TD0000">--Seleccionar--</option>
                    <option id="TD1200">12:00</option>
                    <option id="TD1230">12:30</option>
                    <option id="TD1300">13:00</option>
                    <option id="TD1330">13:30</option>
                    <option id="TD1400">14:00</option>
                    <option id="TD1430">14:30</option>
                    <option id="TD1500">15:00</option>
                    <option id="TD1530">15:30</option>
                    <option id="TD1600">16:00</option>
                    <option id="TD1630">16:30</option>
                    <option id="TD1700">17:00</option>
                    <option id="TD1730">17:30</option>
                </select>
            </div>
            <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                @if (!String.IsNullOrEmpty(formModel.tardeDesde))
                {
                    <label for="tardeHasta">Hasta </label>
                    <select id="tardeHasta" class="form-select" value="@formModel.tardeHasta" @onchange="onChangeTardeHasta">
                        @foreach (var horario in afternoonEndOptions)
                        {
                            <option id=@horario.Replace(":","") value="@horario" selected="@(formModel.tardeHasta == horario)">@horario</option>
                        }
                    </select>
                }
            </div>
        </div>

        @if ((totalMorningMinutes + totalAfternoonMinutes) > 120)
        {
            <div class="alert alert-danger mt-3" role="alert">
                <strong>Recuerde!</strong> Solo se puede seleccionar un período máximo de 2 horas.
            </div>
        }

        <div class="col-12">
            <button type="button" class="btn btn-default btn-tag" @onclick="clearHours">Borrar horas</button>
        </div>
    </div>

    <div class="row m-t-1">
        <div class="col-12 col-md-12 col-lg-12 col-xl-3">
            <h5 for="FechaAAutorizar">Fecha de nacimiento <span class="text-danger">*</span></h5>
            <input id="FechaAAutorizar" class="form-control mb-3" type="date" value="@authorizedDate" bind="formModel.authorizedDateFrom" @onchange="onChangeDateFrom" />
        </div>
        <div class="col-12 col-md-12 col-lg-12 col-xl-3">
            <h5 for="fechaHasta">Fecha de Finalización <span class="text-danger">*</span></h5>
            <input id="fechaHasta" class="form-control mb-3" type="date" min=@minEndDate @bind-value="formModel.authorizedDateTo" readonly />
        </div>
    </div>

    <div class="row m-t-1">
        <div class="col-12 col-md-12 col-lg-12 col-xl-3 mt-3">
            <h5 for="formFile" class="form-label mb-2">Certificado de nacimiento <span class="text-danger">*</span> </h5>
            <button class="btn btn-primary btn-sm" id="uploadButton" @onclick="browseFile"><i class="fa fa-upload"></i> Seleccionar archivo </button>
            <InputFile id="fileInput" class="mb-3" OnChange="@loadFiles" hidden="hidden" required />
        </div>

        @if (formModel.fileName != "" && formModel.fileName != null)
        {
            <div class="col-12 col-md-12 col-lg-12 col-xl-3 mt-3">
                <div class="alert alert-light border" style="text-align:center" id="alertFile" role="alert">
                    <div style="font-size: 20px; font-weight: 800; letter-spacing: 1px; color: #242c4f;">
                        <i class="fa fa-file"></i> @formModel.fileName
                    </div>
                </div>
            </div>

            <div class="col-12 col-md-12 col-lg-12 col-xl-3">
                <button class="btn btn-danger active mt-5" @onclick="deleteFile"><i class="fa fa-trash"></i> </button>
            </div>
        }
    </div>
</EditForm>

<hr />

<div class="d-flex flex-row-reverse">
    <div class="p-2">
        @if(!isFormSubmitted)
        {
            <button class="btn-success btn next-step" @onclick="@(() => confirmData())" data-step="1">Enviar</button>
        }
        else
        {
            <button class="btn-success btn next-step buttonload" disabled data-step="1"><i class="fa fa-spinner fa-spin"></i> Enviando</button>
        }
    </div>
</div>

@code {
    formModel formModel { get; set; } = new formModel();
    [Parameter]
    public EventCallback<formModel> OnFormSubmit { get; set; }
    ConfigurationBuilderForms configBuilder { get; set; } = new ConfigurationBuilderForms();
    public DateTime currentDate = UtilityFunctions.GetCurrentDate();
    private List<IBrowserFile> uploadedFiles = new();
    public List<string> morningEndTimes = new List<string>() {"07:00","07:30","08:00","08:30","09:00","09:30","10:00","10:30","11:00","11:30","12:00"};
    public List<string> morningEndOptions = new List<string>();
    public List<string> afternoonEndTimes = new List<string>() {"12:00","12:30","13:00","13:30","14:00","14:30","15:00","15:30","16:00","16:30","17:00","17:30","18:00"};
    [CascadingParameter]
    private Task<AuthenticationState>? authState { get; set; }
    public string userID { get; set; }
    public List<string> afternoonEndOptions = new List<string>();
    public string minStartDate { get; set; } = "";
    public string minEndDate { get; set; } = "";
    public string authorizedDate { get; set; } = "";
    public bool isFormSubmitted { get; set; } = false;
    public int totalMorningMinutes { get; set; } = 0;
    public int totalAfternoonMinutes { get; set; } = 0;

    protected override async Task OnInitializedAsync()
    {
        var authStateData = await authState;
        var user = authStateData.User;
        userID = user.FindFirst("sub").Value;
        configBuilder = await Functions.GetJsonConfig();
        minStartDate = currentDate.ToString("yyyy-MM-dd");
        authorizedDate = minStartDate;
        minEndDate = authorizedDate;
    }

    private void onChangeManianaDesde(ChangeEventArgs e)
    {
        var selectedTime = e.Value.ToString();
        formModel.manianaDesde = selectedTime;
        CalculateMorningEndTime();
    }

    private void onChangeManianaHasta(ChangeEventArgs e)
    {
        formModel.manianaHasta = e.Value.ToString();
    }

    private void CalculateMorningEndTime()
    {
        if (!string.IsNullOrEmpty(formModel.manianaDesde))
        {
            var startTime = DateTime.ParseExact(formModel.manianaDesde, "HH:mm", CultureInfo.InvariantCulture);
            morningEndOptions = morningEndTimes.Where(time => DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture) > startTime).ToList();
        }
        else
        {
            morningEndOptions = new List<string>();
        }

        CalculateTotalMorningMinutes();
    }

    private void onChangeTardeDesde(ChangeEventArgs e)
    {
        var selectedTime = e.Value.ToString();
        formModel.tardeDesde = selectedTime;
        CalculateAfternoonEndTime();
    }

    private void onChangeTardeHasta(ChangeEventArgs e)
    {
        formModel.tardeHasta = e.Value.ToString();
    }

    private void CalculateAfternoonEndTime()
    {
        if (!string.IsNullOrEmpty(formModel.tardeDesde))
        {
            var startTime = DateTime.ParseExact(formModel.tardeDesde, "HH:mm", CultureInfo.InvariantCulture);
            afternoonEndOptions = afternoonEndTimes.Where(time => DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture) > startTime).ToList();
        }
        else
        {
            afternoonEndOptions = new List<string>();
        }

        CalculateTotalAfternoonMinutes();
    }

    private void CalculateTotalMorningMinutes()
    {
        if (!string.IsNullOrEmpty(formModel.manianaDesde) && !string.IsNullOrEmpty(formModel.manianaHasta))
        {
            var startTime = DateTime.ParseExact(formModel.manianaDesde, "HH:mm", CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(formModel.manianaHasta, "HH:mm", CultureInfo.InvariantCulture);
            totalMorningMinutes = (int)(endTime - startTime).TotalMinutes;
        }
        else
        {
            totalMorningMinutes = 0;
        }
    }

    private void CalculateTotalAfternoonMinutes()
    {
        if (!string.IsNullOrEmpty(formModel.tardeDesde) && !string.IsNullOrEmpty(formModel.tardeHasta))
        {
            var startTime = DateTime.ParseExact(formModel.tardeDesde, "HH:mm", CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(formModel.tardeHasta, "HH:mm", CultureInfo.InvariantCulture);
            totalAfternoonMinutes = (int)(endTime - startTime).TotalMinutes;
        }
        else
        {
            totalAfternoonMinutes = 0;
        }
    }

    private void clearHours()
    {
        formModel.manianaDesde = "";
        formModel.manianaHasta = "";
        formModel.tardeDesde = "";
        formModel.tardeHasta = "";
        morningEndOptions.Clear();
        afternoonEndOptions.Clear();
    }

    private async void browseFile()
    {
        await jsRuntime.InvokeVoidAsync("document.getElementById", "fileInput").InvokeVoidAsync("click");
    }

    private async Task loadFiles(InputFileChangeEventArgs e)
    {
        var files = e.GetMultipleFiles();
        uploadedFiles.Clear();
        uploadedFiles.AddRange(files);
        formModel.fileName = files[0].Name;
    }

    private void deleteFile()
    {
        uploadedFiles.Clear();
        formModel.fileName = "";
    }

    private async Task onChangeDateFrom(ChangeEventArgs e)
    {
        var selectedDate = e.Value.ToString();
        formModel.authorizedDateFrom = selectedDate;
        minEndDate = selectedDate;
    }

    private async Task confirmData()
    {
        isFormSubmitted = true;
        await Task.Delay(2000);
        isFormSubmitted = false;

        await OnFormSubmit.InvokeAsync(formModel);
    }

    public class formModel
    {
        public string manianaDesde { get; set; }
        public string manianaHasta { get; set; }
        public string tardeDesde { get; set; }
        public string tardeHasta { get; set; }
        public string authorizedDateFrom { get; set; }
        public string authorizedDateTo { get; set; }
        public string fileName { get; set; }
    }
}
