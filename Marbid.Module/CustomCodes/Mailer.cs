using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Marbid.Module.CustomCodes
{
  class Mailer
  {
    string _emailFrom;
    string _emailTo;
    string _Subject;
    string _Body;
    string _cc = null;
    bool _BodyFormat = true;
    MailMessage _mail;

    public Mailer(string EmailFrom, string EmailTo, string Subject, string Body)
    {
      _emailFrom = EmailFrom;
      _emailTo = EmailTo;
      _Subject = Subject;
      _Body = Body;
    }

    public Mailer(MailMessage mail)
    {
      _mail = mail;
    }

    public bool BodyFormat
    {
      get { return _BodyFormat; }
      set { _BodyFormat = value; }
    }
    public string EmailFrom
    {
      get { return _emailFrom; }
      set { _emailFrom = value; }
    }

    public string Subject
    {
      get { return _Subject; }
      set { _Subject = value; }
    }

    public string Body
    {
      get { return _Body; }
      set { _Body = value; }
    }

    public string EmailTo
    {
      get { return _emailTo; }
      set { _emailTo = value; }
    }

    public string CC
    {
      get { return _cc; }
      set { _cc = value; }
    }

    public bool SendMail()
    {
      bool IsOkay = true;

      MailMessage mail = new MailMessage();
      SmtpClient smtp = new SmtpClient("192.168.2.206");

      mail.From = new MailAddress("noreply-marbid@marein-re.com");
      mail.To.Add(_emailTo);
      mail.Subject = _Subject;
      mail.Body = _Body;
      mail.IsBodyHtml = _BodyFormat;
      if (_cc != null)
      {
        mail.CC.Add(_cc);
      }
      
      try
      {
        smtp.Send(mail);
      }
      catch (Exception ex)
      {
        IsOkay = false;
        Console.WriteLine(ex.ToString());
      }

      return IsOkay;
    }
  }
}
