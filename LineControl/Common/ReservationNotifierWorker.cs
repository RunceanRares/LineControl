using LineControllerInfrastructure;

using Microsoft.EntityFrameworkCore;

namespace LineControl.Common
{
  public class ReservationNotifierWorker : BackgroundService
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ReservationNotifierWorker> _logger;

    public ReservationNotifierWorker(IServiceScopeFactory scopeFactory, ILogger<ReservationNotifierWorker> logger)
    {
      _scopeFactory = scopeFactory;
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Serviciul de notificare rezervări a pornit.");

      while (!stoppingToken.IsCancellationRequested)
      {
        try
        {
          await CheckAndSendNotificationsAsync(stoppingToken);
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "Eroare critică în procesul de verificare a rezervărilor.");
        }

        // Așteaptă 24 de ore până la următoarea verificare
        // Calculate time until next run (e.g., next day at 8 AM) or simple delay
        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
      }
    }
    private async Task CheckAndSendNotificationsAsync(CancellationToken token)
    {
      // BackgroundService este Singleton, dar DbContext este Scoped.
      // Trebuie să creăm un scope manual pentru a putea folosi baza de date.
      using (var scope = _scopeFactory.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<LineContextDb>();

        // Dacă ai un serviciu de mail, îl iei tot de aici
        // var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var daysUntilExpiration = 7;
        var targetDate = DateTime.Today.AddDays(daysUntilExpiration);

        // Căutăm rezervările ACTIVE care expiră fix peste 7 zile
        // Presupunem că StatusId pentru Open este o constantă cunoscută sau o iei din DB
        int openStatusId = 1; // Înlocuiește cu valoarea corectă din ReservationStatusViewModel.OpenId

        var expiringReservations = await context.DeviceReservations
            .Include(r => r.CreatedBy) // Include User-ul care a creat rezervarea (pentru email)
            .Include(r => r.Device)    // Include Device-ul (pentru ItemNumber)
            .Where(r => r.StatusId == openStatusId
                        && r.EndDate != null
                        && r.EndDate.Value.Date == targetDate)
            .ToListAsync(token);

        if (!expiringReservations.Any())
        {
          _logger.LogInformation("Nicio rezervare nu expiră peste 7 zile.");
          return;
        }

        foreach (var reservation in expiringReservations)
        {
          // Aici trimiți emailul
          var userEmail = reservation.CreatedBy?.Email; // Sau proprietatea unde ții mailul
          var deviceName = reservation.Device?.ItemNumber;

          if (!string.IsNullOrEmpty(userEmail))
          {
            string subject = $"Reminder: Rezervarea expiră în {daysUntilExpiration} zile";
            string body = $"Salut {reservation.CreatedBy.UserName},\n\n" +
                          $"Rezervarea pentru dispozitivul {deviceName} expiră pe data de {reservation.EndDate:dd/MM/yyyy}.\n" +
                          $"Te rugăm să îl returnezi sau să prelungești rezervarea.";

            // Exemplu apel serviciu email (trebuie să implementezi partea asta):
            // await emailService.SendEmailAsync(userEmail, subject, body);

            _logger.LogInformation($"[Notificare] Email trimis către {userEmail} pentru dispozitivul {deviceName}.");
          }
        }
      }
    }
  }
}
