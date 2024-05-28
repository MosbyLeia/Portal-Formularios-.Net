@attribute [Authorize]
@using ProyectoPortalTramitesServer.Models
@using ProyectoPortalTramitesServer.Services.ApiPortalTramites.IRepository
@using apiPortalTramites.Data
@inject IJSRuntime js
@inject IServicioVacacionesRequest request
@inject IToastService toastService
@inject ILoggerManager logger

<EditForm Model="model">

    @if (model.startDate != null)
    {
        <div class="alert alert-warning alert-dismissible fade show" role="alert">
            Se informa fecha límite de licencia.
        </div>

        <div class="row m-t-1">
            <div class="col-12">
                <div class="row">
                    <div class="col-4">
                        <label for="startDate">Fecha de inicio <span class="text-danger">*</span></label>
                        <input id="startDate" class="form-control" min=@minDate type="date" @bind-value="model.startDate" required />
                    </div>
                    <div class="col-4" hidden="@showEndDate">
                        @if (totalDays > 0)
                        {
                            model.endDate = model.startDate.AddDays((double)totalDays - 1)
                            <label for="endDate" id="endDate">Fecha de finalización</label>
                            <input id="endDate" class="form-control" type="date" @bind-value="model.endDate" readonly />
                        }
                    </div>
                </div>

                @if (model.startDate < today)
                {
                    <div class="invalid-feedback" style="display:block;">
                        No se puede enviar un formulario con una fecha anterior a la de hoy. Por favor, volver a reintentar cambiando la fecha.
                    </div>
                }
                else
                {
                    <div class="row mt-5">
                        <div class="col-12">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Año</th>
                                        <th>Disponible</th>
                                        <th>Pedido</th>
                                        <th>Fracción</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @if (periods.Count > 0)
                                    {
                                        @foreach (var period in periods)
                                        {
                                            <tr id=@period.id>
                                                <td>@period.year</td>
                                                <td>@period.available</td>
                                                <td>
                                                    <input type="number" class="form-control" id="@period.year" style="@(IsFocused(period.year) ? "border: 2px solid #3498db;" : "")"
                                                           @bind="period.requested" disabled="@period.isEditing" @ref="inputPeriod"
                                                           @onfocusin="(() => FocusInput(period.year))" />

                                                    @if (period.isEditing)
                                                    {
                                                        @if (period.requested > period.available)
                                                        {
                                                            <div class="invalid-feedback" style="display:block;">
                                                                Cantidad de días pedidos no puede superar los @period.available disponibles.
                                                            </div>
                                                        }
                                                    }
                                                </td>
                                                <td>@period.fraction</td>
                                                <td class="text-end">
                                                    @if (period.isSelected)
                                                    {
                                                        <button class="btn btn-success" @onclick="(() => EnableEditing(period))">
                                                            <i class="fa fa-pencil-square-o"></i>
                                                        </button>
                                                    }
                                                    else
                                                    {
                                                        @if (!period.isEditing)
                                                        {
                                                            <button class="btn btn-success" @onclick="(() => SaveEditing(period))">
                                                                <i class="fa fa-save"></i>
                                                            </button>
                                                        }
                                                    }
                                                </td>
                                            </tr>
                                        }
                                    }
                                    else
                                    {
                                        <tr>
                                            <td class="text-muted" colspan="5">Usted no tiene periodos disponibles</td>
                                        </tr>
                                    }
                                </tbody>
                            </table>

                            <div class="col-12">
                                <button type="button" class="btn btn-default btn-tag" disabled="@(!validateButton)" @onclick="ValidateDates">Validar fechas</button>
                            </div>
                        </div>
                    </div>
                }
            </div>
        </div>
    }
</EditForm>

<hr />
<div class="d-flex flex-row-reverse">
    <div class="p-2">
        @if (dataComplete)
        {
            @if (!formSubmitted)
            {
                <button class="btn-success btn next-step" @onclick="@(() => ConfirmData())" data-step="1">Enviar</button>
            }
            else
            {
                <button class="btn-success btn next-step buttonload" disabled data-step="1"><i class="fa fa-spinner fa-spin"></i> Enviando</button>
            }
        }
        else
        {
            <button class="btn btn-secondary text-decoration-none" data-step="1" disabled>Enviar</button>
        }
    </div>
</div>

@code {
    Model model { get; set; } = new Model();
    [CascadingParameter] private Task<AuthenticationState>? authState { get; set; }
    public string userId { get; set; }
    [Parameter] public EventCallback<(Model, List<Period>)> OnComplete { get; set; }
    public bool dataComplete { get; set; } = false;
    public string minDate { get; set; } = "";
    public int? totalDays { get; set; } = 0;
    private bool showEndDate = true;
    private ElementReference inputPeriod;
    private bool isFocused;
    private int focusedRow { get; set; } = -1;
    bool formSubmitted { get; set; } = false;
    public DateTime today = DateTime.Today;
    List<Period> periods { get; set; } = new List<Period>();

    protected override async Task OnInitializedAsync()
    {
        var auth = await authState;
        var user = auth?.User;
        userId = user?.Identity?.Name ?? "unknown";
        minDate = DateTime.Today.ToString("yyyy-MM-dd");
        model.startDate = DateTime.Today;
        await InvokeAsync(() => StateHasChanged());
    }

    protected override async Task OnAfterRenderAsync(bool render)
    {
        if (render)
        {
            try
            {
                var response = await request.GetNewRequestAsync($"/api/VacationService/GetNewRequestAsync/{userId}");
                if (response != null && response.Success)
                {
                    InitializeData(response);
                    foreach (var year in response.Data.Years)
                    {
                        periods.Add(new Period
                        {
                            year = year.Name,
                            available = year.Available,
                            requested = year.Requested,
                            fraction = year.Fraction,
                            isSelected = year.IsSelected ?? true,
                            isEditing = true
                        });
                    }

                    foreach (var period in periods)
                    {
                        period.isSelected = false;
                    }
                    periods.First().isSelected = true;
                }
            }
            catch (Exception e)
            {
                logger.LogError("Error loading form", e);
            }

            if (formSubmitted && !dataComplete)
            {
                formSubmitted = dataComplete;
                await InvokeAsync(() => StateHasChanged());
            }
        }
    }

    private async Task EnableEditing(Period period)
    {
        try
        {
            dataComplete = false;
            validateButton = false;

            if (period.fraction == "Segunda fracción")
            {
                period.requested = period.available;
            }

            var index = periods.IndexOf(period);
            if (index < periods.Count - 1 && periods.ElementAt(index + 1) != null && periods.ElementAt(index + 1).requested > 0)
            {
                toastService.ShowError($"No es posible editar el periodo {period.year} ya que existen días seleccionados para el año {periods.ElementAt(index + 1).year}.", userId, "Atención");
            }
            else
            {
                if (index > 0 && periods.Count > 1)
                {
                    if (periods.ElementAt(index - 1).isEditing)
                    {
                        if (periods.ElementAt(index - 1).available == periods.ElementAt(index - 1).requested)
                        {
                            period.isSelected = false;
                            period.isEditing = false;
                        }
                        else
                        {
                            toastService.ShowWarning("Debe agotar la totalidad de los días de los periodos anteriores para poder solicitar el seleccionado", userId, "Atención");
                        }
                    }
                }
                else
                {
                    period.isSelected = false;
                    period.isEditing = false;
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError("Error enabling editing", e);
        }
    }

    private async Task SaveEditing(Period period)
    {
        period.isEditing = true;
        period.isSelected = true;

        try
        {
            if (period.requested > 0)
            {
                if (period.requested <= period.available)
                {
                    await CalculateDays();
                    period.isEditing = true;
                    period.isSelected = true;
                }
                else
                {
                    period.requested = 0;
                    toastService.ShowWarning("Los días solicitados superan los disponibles, favor de corregir.", userId, "Atención");
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError("Error saving editing", e);
        }
    }

    private async Task CalculateDays()
    {
        try
        {
            if (periods != null && periods.Count > 0)
            {
                totalDays = 0;
                foreach (var item in periods)
                {
                    totalDays += item.requested;
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError("Error calculating days", e);
        }
    }

    private bool IsFocused(int year)
    {
        return year == focusedRow;
    }

    private async Task FocusInput(int year)
    {
        focusedRow = year;
    }

    public void ValidateDates()
    {
        dataComplete = model.startDate >= today && model.startDate.AddDays((double)totalDays - 1) <= model.endDate;
    }

    private void ConfirmData()
    {
        dataComplete = true;
        formSubmitted = true;
        OnComplete.InvokeAsync((model, periods));
    }

    private void InitializeData(Response response)
    {
        if (response.Data.StartDate != null)
        {
            model.startDate = (DateTime)response.Data.StartDate;
            model.endDate = model.startDate.AddDays((double)response.Data.TotalDays - 1);
            totalDays = response.Data.TotalDays;
            showEndDate = false;
        }
    }
}

public class Model
{
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}

public class Period
{
    public int id { get; set; }
    public string year { get; set; }
    public int available { get; set; }
    public int requested { get; set; }
    public string fraction { get; set; }
    public bool isSelected { get; set; }
    public bool isEditing { get; set; }
}
