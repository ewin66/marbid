using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Marbid.Module.CustomCodes
{
   public class CodeLibrary
   {
      public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
      {
         var ratioX = (double)maxWidth / image.Width;
         var ratioY = (double)maxHeight / image.Height;
         var ratio = Math.Min(ratioX, ratioY);

         var newWidth = (int)(image.Width * ratio);
         var newHeight = (int)(image.Height * ratio);

         var newImage = new Bitmap(newWidth, newHeight);

         using (var graphics = Graphics.FromImage(newImage))
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);

         return newImage;
      }
   }

  public class Calculator
  {


    public Calculator() {

    }

    public void TimeSpanToDate(DateTime d1, DateTime d2, out int years, out int months, out int days)
    {
        // compute & return the difference of two dates,
        // returning years, months & days
        // d1 should be the larger (newest) of the two dates
        // we want d1 to be the larger (newest) date
        // flip if we need to
        if (d1 < d2)
        {
            DateTime d3 = d2;
            d2 = d1;
            d1 = d3;
        }

        // compute difference in total months
        months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

        // based upon the 'days',
        // adjust months & compute actual days difference
        if (d1.Day < d2.Day)
        {
            months--;
            days = DateTime.DaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
        }
        else
        {
            days = d1.Day - d2.Day;
        }
        // compute years & actual months
        years = months / 12;
        months -= years * 12;
    }

    public int Age(DateTime BirthDate, DateTime ToDate)
    {
        int age = ToDate.Year - BirthDate.Year;
        if (BirthDate > ToDate.AddYears(-age)) age--;
        return age;
    }

    public string TimeSpanToDate(DateTime d1, DateTime d2)
    {
        int years = 0;
        int months = 0;
        int days = 0;
        TimeSpanToDate(d1, d2, out years, out months, out days);
        string yearlabel = "year";
        string monthlabel = "month";
        string daylabel = "day";
        if (years > 1) { yearlabel = "years"; } 
        if (months > 1) { monthlabel = "months"; }
        if (days > 1) { daylabel = "days"; }

        return string.Format("{0} {1} {2} {3} {4} {5}", years.ToString(), yearlabel, months.ToString(), monthlabel, days.ToString(), daylabel);
    }  

   public int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
    {
      firstDay = firstDay.Date;
      lastDay = lastDay.Date;
      if (firstDay > lastDay)
        throw new ArgumentException("Incorrect last day " + lastDay);

      TimeSpan span = lastDay - firstDay;
      int businessDays = span.Days + 1;
      int fullWeekCount = businessDays / 7;
      // find out if there are weekends during the time exceedng the full weeks
      if (businessDays > fullWeekCount * 7)
      {
        // we are here to find out if there is a 1-day or 2-days weekend
        // in the time interval remaining after subtracting the complete weeks
        int firstDayOfWeek = (int)firstDay.DayOfWeek;
        int lastDayOfWeek = (int)lastDay.DayOfWeek;
        if (lastDayOfWeek < firstDayOfWeek)
          lastDayOfWeek += 7;
        if (firstDayOfWeek <= 6)
        {
          if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
            businessDays -= 2;
          else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
            businessDays -= 1;
        }
        else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
          businessDays -= 1;
      }

      // subtract the weekends during the full weeks in the interval
      businessDays -= fullWeekCount + fullWeekCount;

      // subtract the number of bank holidays during the time interval
     if (bankHolidays.Length > 0)
       foreach (DateTime bankHoliday in bankHolidays)
       {
         DateTime bh = bankHoliday.Date;
         if (firstDay <= bh && bh <= lastDay)
           --businessDays;
       }

      return businessDays;
    }
  }
  }



