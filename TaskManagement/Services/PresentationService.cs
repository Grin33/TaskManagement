using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace TaskManagement.Services
{
  /// <summary>
  /// Сервис формирования отчетов
  /// </summary>
  public static class PresentationService
  {
    //можно сделать через рефлексию и кастомные аттрибуты
    public const int TaskColumns = 5;
    public static MemoryStream toPDF(IEnumerable<Models.Task> tasks, string username)
    {
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      
      var doc = new iTextSharp.text.Document();
      var stream = new MemoryStream();
      var pdfWriter = PdfWriter.GetInstance(doc, stream);
      pdfWriter.CloseStream = false;
      //PdfWriter.GetInstance(doc, new FileStream($"{username}_TasksReport_{DateTime.Now.ToString("mmHHMMyyyy")}.pdf", FileMode.Create));
      try
      {
        doc.Open();

        var baseFont = BaseFont.CreateFont(@"c:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        var font = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        var table = new PdfPTable(TaskColumns);

        var cell = new PdfPCell(new Phrase($"Все задачи для пользователя {username}", font));
        cell.Colspan = TaskColumns;
        cell.HorizontalAlignment = 1;
        cell.Border = 0;
        table.AddCell(cell);


        var displayAttr = typeof(Models.Task).GetProperty("Name").GetCustomAttributes(false).Where(t => t is DisplayAttribute).FirstOrDefault() as DisplayAttribute;
        var name = displayAttr == null ? "Неопознанное свойство" : displayAttr.Name;
        cell = new PdfPCell(new Phrase(new Phrase(name, font)));
        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
        table.AddCell(cell);

        displayAttr = typeof(Models.Task).GetProperty("Description").GetCustomAttributes(false).Where(t => t is DisplayAttribute).FirstOrDefault() as DisplayAttribute;
        name = displayAttr == null ? "Неопознанное свойство" : displayAttr.Name;
        cell = new PdfPCell(new Phrase(new Phrase(name, font)));
        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
        table.AddCell(cell);

        displayAttr = typeof(Models.Task).GetProperty("Priority").GetCustomAttributes(false).Where(t => t is DisplayAttribute).FirstOrDefault() as DisplayAttribute;
        name = displayAttr == null ? "Неопознанное свойство" : displayAttr.Name;
        cell = new PdfPCell(new Phrase(new Phrase(name, font)));
        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
        table.AddCell(cell);

        displayAttr = typeof(Models.Task).GetProperty("Status").GetCustomAttributes(false).Where(t => t is DisplayAttribute).FirstOrDefault() as DisplayAttribute;
        name = displayAttr == null ? "Неопознанное свойство" : displayAttr.Name;
        cell = new PdfPCell(new Phrase(new Phrase(name, font)));
        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
        table.AddCell(cell);

        displayAttr = typeof(Models.Task).GetProperty("DeadLine").GetCustomAttributes(false).Where(t => t is DisplayAttribute).FirstOrDefault() as DisplayAttribute;
        name = displayAttr == null ? "Неопознанное свойство" : displayAttr.Name;
        cell = new PdfPCell(new Phrase(new Phrase(name, font)));
        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
        table.AddCell(cell);

        foreach(var task in tasks)
        {
          if(task == null)
            continue;
          table.AddCell(new Phrase($"{task.Name}", font));
          table.AddCell(new Phrase($"{task.Description}", font));
          table.AddCell(new Phrase($"{task.Priority}", font));
          table.AddCell(new Phrase($"{task.Status}", font));
          table.AddCell(new Phrase($"{task.DeadLine:dd.MM.yyyy}", font));
        }

        doc.Add(table);

        doc.Close();
      }
      catch 
      {
        doc.Close();
      }
      var byteinfo = stream.ToArray();
      stream.Write(byteinfo, 0, byteinfo.Length);
      //stream.Flush();
      stream.Position = 0;

      return stream;
      //return new FileStreamResult(stream, "application/pdf", $"{username}_TasksReport_{DateTime.Now.ToString("mmHHMMyyyy")}.pdf");

    }
  }
}
