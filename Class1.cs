using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP

{

    public interface Subjects
    {
        public void registerObserver(Observer observer);
        public void deleteObserver(Observer observer);
        public void notifyObserver();
    }

    public interface Observer
    {
        public void update(float temperatura, float humidity, float pressure);
    }


    public interface Display1
    {
        public void display();
    }

    public class Weather : Subjects
    {
        private List<Observer> observers = new List<Observer>();
        private float temperatura, humidity, pressure;
        public void registerObserver(Observer obs)
        {
            observers.Add(obs);
        }

        public void deleteObserver(Observer observer)
        {
            int i = observers.IndexOf(observer);
            if( i >= 0)
            {
                observers.RemoveAt(i);
            }
        }

        public void notifyObserver()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                Observer obs = observers[i];
                obs.update(temperatura, humidity, pressure);
            }
        }
        public void measurementsChanged()
        {
            notifyObserver();
        }

        public void setMeasurements(float temperatura, float humidity, float pressure)
        {
            this.temperatura = temperatura;
            this.humidity = humidity;
            this.pressure = pressure;
            measurementsChanged();
        }
    }

    public class CurrentConditionsDisplay : Observer, Display1
    {
        private float temperatura, humidity, pressure;
        private Subjects Weather;

        public CurrentConditionsDisplay(Subjects Weather)
        {
            this.Weather = Weather;
            Weather.registerObserver(this);
        }

        public void update(float temperatura, float humidity, float pressure)
        {
            this.temperatura = temperatura;
            this.humidity = humidity;
            this.pressure = pressure;
            display();
        }

        public void display()
        {
            Console.WriteLine("Now weather:" + temperatura + " celcius" + humidity + " humidity " + pressure + " pressure");
        }
    }

    public class StatisticDisplay : Observer, Display1
    {
        private float temper = 0.0f, mint = 200, maxt = 0.0f;
        int numReadings;
        private Subjects Weather;
        public StatisticDisplay(Subjects Weather)
        {
            this.Weather = Weather;
            Weather.registerObserver(this);
        }

        public void update(float temperatura, float humidity, float pressure)
        {
            temper += temperatura;
            numReadings++;
            
            if (temper > maxt)
            {
                maxt = temperatura;
            }

            else
            {
                temperatura = mint;
            }

            display();
        }

        public void display()
        {
            Console.WriteLine("Temperatura" + (temper / numReadings));
        }

    }

    public class ForecastDisplay : Observer, Display1
    {
        private float temp, humidity, pressure;
        private Subjects Weather;
        public ForecastDisplay(Subjects Weather)
        {
            this.Weather = Weather;
            Weather.registerObserver(this);
        }

        public void update(float temp, float humidity, float pressure)
        {
            this.temp = temp;
            this.humidity = humidity;
            this.pressure = pressure;
            display();
        }

        public void display()
        {
            Console.WriteLine("Temperatura will " + (temp + 4));
        }

    }



    public class Class1
    {
        public static void Main(String[] args)
        {
            Weather Weather = new Weather();
            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(Weather);
            StatisticDisplay statsDisplay = new StatisticDisplay(Weather);
            ForecastDisplay forecastDisplay = new ForecastDisplay(Weather);
            Weather.setMeasurements(24, 65, 30.4f);
            Weather.setMeasurements(25, 70, 29.4f);
            Weather.setMeasurements(26, 90, 29.2f);
        }
    }
}
