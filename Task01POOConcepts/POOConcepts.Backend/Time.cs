using System.Globalization;

namespace POOConcepts.Backend;

public class Time
{               //Private Fields.
    private int _hour;
    private int _millisecond;
    private int _minute;
    private int _second;

    //Builder Method and overloads.
    public Time()
    {
        Hour = 00;
        Minute = 00;
        Second = 00;
        Millisecond = 000;
    }

    public Time(int hour)
    {
        Hour = hour;
    }

    public Time(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public Time(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    //Public properties.
    public int Hour
    {
        get => _hour;
        set => _hour = ValidHour(value);
    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidMillisecond(value);
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidSecond(value);
    }

    public override string ToString()
    {
        DateTime dt = new DateTime(1, 1, 1, Hour, Minute, Second, Millisecond);
        String result = dt.ToString("hh:mm:ss.fff tt", new CultureInfo("en-US"));

        if (Hour == 0)
        {
            result = "00" + result.Substring(2);
        }
        return result;
    }

    //Methods.

    public long ToMilliseconds() => (Hour * 3600000L) + (Minute * 60000L) + (Second * 1000L) + Millisecond;

    public long ToSeconds() => (Hour * 3600L) + (Minute * 60L) + Second;

    public long ToMinutes() => (Hour * 60L) + Minute;

    public bool IsOtherDay(Time t4)
    {
        long total = this.ToMilliseconds() + t4.ToMilliseconds();
        return total >= 24 * 3600000L;
    }

    public Time Add(Time t3)
    {
        int newMs = this.Millisecond + t3.Millisecond;
        int carrySec = newMs / 1000;
        newMs %= 1000;

        int newSec = this.Second + t3.Second + carrySec;
        int carryMin = newSec / 60;
        newSec %= 60;

        int newMin = this.Minute + t3.Minute + carryMin;
        int carryHour = newMin / 60;
        newMin %= 60;

        int newHour = this.Hour + t3.Hour + carryHour;
        newHour %= 24;

        return new Time(newHour, newMin, newSec, newMs);
    }

    //Validation methods.

    private int ValidHour(int Hour)
    {
        if (Hour < 0 || Hour > 23)
        {
            throw new Exception($"The hour: {Hour}, is not valid\n");
        }
        return Hour;
    }

    private int ValidMillisecond(int Millisecond)
    {
        if (Millisecond < 0 || Millisecond > 999)
        {
            throw new Exception($"The milliseconds: {Millisecond}, is not valid\n");
        }
        return Millisecond;
    }

    private int ValidMinute(int Minute)
    {
        if (Minute < 0 || Minute > 59)
        {
            throw new Exception($"The minutes: {Minute}, is not valid\n");
        }
        return Minute;
    }

    private int ValidSecond(int Second)
    {
        if (Second < 0 || Second > 59)
        {
            throw new Exception($"The seconds: {Second}, is not valid\n");
        }
        return Second;
    }
}