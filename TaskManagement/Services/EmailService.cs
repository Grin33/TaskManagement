using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using TaskManagement.Models;
using TaskManagement.Data.Enums;

namespace TaskManagement.Services
{
  public static class EmailService
  {

    /// <summary>
    /// Отправка Email по адресу эл.почты
    /// </summary>
    /// <param name="recieveAdress">адрес получателя</param>
    /// <param name="TaskName">Имя задачи по которой отправляется уведомление</param>
    /// <param name="reasonStr">Причина отправки</param>
    private static void SendEmail(string recieveAdress, string TaskName, string reasonStr)
    {
      System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
			try
      {
        //helloworlD1 - реальный
				//rxsm cmci qazj cnym - пароль приложения
				System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        message.IsBodyHtml = true; //тело сообщения в формате HTML
        message.From = new MailAddress("taskmanagementnotifier@gmail.com", "TMN");
        message.To.Add(recieveAdress);
        message.Subject = "TMS notify";
        message.Body = $"<h1>{TaskName}<h1>";
        message.Body += $"<p>Состояние задачи: {reasonStr}<p>"; 

        using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) 
        {
          client.Credentials = new NetworkCredential("taskmanagementnotifier@gmail.com", "rxsm cmci qazj cnym");
          client.Port = 587; //порт 587 либо 465
          client.EnableSsl = true;

          client.Send(message);
        }
        }
      catch
      {
        throw;
      }
    }

    /// <summary>
    /// Метод, запускающий отправку Email Списку пользователей
    /// </summary>
    /// <param name="users">Список пользователей</param>
    /// <param name="task">Задача, по которой будет отправляться уведомление</param>
    /// <param name="reason">Причина отправки</param>
    public static void sendEmailToUsers(IEnumerable<User?> users, Models.Task task, Reason reason)
    {
      string reasonStr;
      switch(reason)
      {
        case Reason.Delete:
          reasonStr = "Удалена";
          break;
        case Reason.Create:
          reasonStr = "Создана";
          break;
        case Reason.Edit:
          reasonStr = "Изменена";
          break;
        case Reason.FeedBack:
          reasonStr = "Требуется обрантая связь";
          break;
        default:
          reasonStr = "Не определено";
          break;
      }
      foreach(var user in users)
      {
        if (user == null)
          continue;
        //все работает, закоменчено чтоб не мешало))
        //SendEmail(user.Email, task.Name, reasonStr);
      }
    }
  }
}
