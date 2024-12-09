using System.Net.Http.Headers;
using static SOLID_Example.Task3_L_Solid;
using static SOLID_Example.Task4_I_Solid;

namespace SOLID_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //new Task3_L_Solid().Print(new ElectricEngine());//exception ok !
            //new Task3_L_Solid().Fly(new Bird());
            //new Task3_L_Solid().Fly(new Penguin());
            IMachine mm = new TM();
            mm.Fax();mm.Scan();mm.Print();
        }
    }

    public class Task5_D_Solid
    {
        #region bulb
        /*
         * Task: Decouple LightSwitch from the concrete Bulb class using an appropriate abstraction.
         */
        public class LightSwitch
        {
            private IBulb bulb { get; set; }
            public void Operate() {
                // Toggle bulb state

            }
        }

        public interface IBulb
        {
            void TurnOn();
            void TurnOff();

        }
        public class Bulb: IBulb
        {
            void IBulb.TurnOn()
            { 

            }
            void IBulb.TurnOff() 
            { 
              
            }
        }


        #endregion

        #region weather
        //Task: Refactor the WeatherReporter class so that it doesn&#39;t depend on a specific temperature sensor implementation.
        public class WeatherReporter
        {
            private ISensor sensor { get; set; }
            public string Report()
            {
                return $"Current temperature: { this.sensor.GetTemperature()} ";
            }
        }


        public interface ISensor
        {
            double GetTemperature();
        }
        public class TemperatureSensor: ISensor
        {
            double ISensor.GetTemperature()
            {
                // Return temperature from sensor
                return 25.0; // dummy value
            }
        }
        #endregion

    }

    public class Task4_I_Solid
    {
        #region machine
        /*
         * Task: Not all machines can print, fax, and scan. Separate these capabilities into individual interfaces.
         * public interface IMachine {
            void Print();
            void Fax();
            void Scan();
            }
         */
        public interface IFax
        {
            void Fax();
        }
        public interface IScan
        {
            void Scan();
        }
       

        public interface IMachine:IFax,IScan
        {
            void Print();
          
           
        }
        public class TM : IMachine
        {
            void IFax.Fax()
            {
                Console.WriteLine("fax");
            }

            void IMachine.Print()
            {
                Console.WriteLine("print");
            }

            void IScan.Scan()
            {
                Console.WriteLine("Scan");
            }
        }
        #endregion
        //-----------------------------------

        #region play
        /*
         * Task: Some media players might only support play and pause. Refactor the interface to
           ensure that no player class implements unnecessary methods.
       public interface IPlayer
        {
            void Play();
            void Pause();
            void Next();
            void Previous();
            void Shuffle();
        }
         */
        #endregion
        //--------------------------------------

        public interface IPlay
        {
            void Play();
            void Pause();
        }

        public interface IPosition
        {
            void Next();
            void Previous();
            void Shuffle();
        }

    }

    public class Task3_L_Solid
    {
        #region engine
        /*
         * Task: Address the violation of LSP in the above inheritance hierarchy.
         * public class Engine 
         * {
            public void Start() 
            {
                // Start the engine
            }
          }
        public class ElectricEngine : Engine 
        {
            public override void Start() 
            {
                throw new Exception(&quot;Electric engines don't start traditionally.&quot;);
            }
        }
         */
        public void Fly(Bird b)
        {
            b.Fly();
        }


        public void Print(Engine k)
        {
            k.Start();
        }

        public class Engine
        {
            public virtual void Start()
            {
                // Start the engine
            }
        }
        public class ElectricEngine : Engine
        {
            public override void Start()
            {
                throw new Exception("Electric engines don't start traditionally.");
            }
        }
        #endregion

        #region birds
        /*
         * Task: Penguins don&#39;t fly! Modify the class design to adhere to LSP.
         * public class Bird {
            public void Fly() {
            //...
            }
            }
            public class Penguin : Bird { }
         */
        public class Bird
        {
            public virtual void Fly()
            {
                Console.WriteLine("bird flies !");
            }
        }
        public class Penguin : Bird 
        {
            public override void Fly()
            {
                Console.WriteLine("no flight !");
            }
        }
        #endregion
    }


    public class Task2_O_Solid
    {
        #region logger
        /*
         * Task: Refactor the Logger class to allow adding more logging methods in the future without changing the existing code.
         
        public class Logger
        {
            public void LogToConsole(string message)
            {
                Console.WriteLine(message);
            }
            public void LogToFile(string message, string filename)
            {
                // Code to write message to a file
            }
        }
        */

        public interface ILoggerFile
        {
            void Log();
        }

        public class Logger
        {
            public void Log(ILoggerFile log)
            {
                log.Log();
            }
        }

        public class LogConsole : ILoggerFile
        {
            public string Caption { get; set; }
            public LogConsole(string caption)
            {
                this.Caption = caption;
            }

            void ILoggerFile.Log()
            {
                Console.WriteLine(this.Caption);
            }

        }

        public class LogFileSSD : ILoggerFile
        {
            public string Caption { get; set; }
            private string mSSD = "";
            public LogFileSSD(string caption, string ssd)
            {
                this.Caption = caption;
                this.mSSD = ssd;
            }

            void ILoggerFile.Log()
            {
                File.WriteAllText(this.mSSD, this.Caption);
            }
        }
        //---------------------------------------------------
        #endregion

        #region  discount
        /*
         * Task: Refactor the DiscountCalculator class so that you can introduce new discount types without modifying existing code.
         * public class DiscountCalculator 
         * {
            public double CalculateDiscount(string type, double price)
            {
            if (type == &quot;STUDENT&quot;) {
            return price * 0.1;
            } else if (type == &quot;SENIOR&quot;) {
            return price * 0.2;
            }
            return price;
            }
            }
         */

        public class Discounter
        {
            public double CalculateDiscount(double price, IDiscount dtype)
            {
                return dtype.Value(price);
            }
        }

        public interface IDiscount
        {
            double Value(double price);
        }

        public class StudentDiscount : IDiscount
        {
            double IDiscount.Value(double price)
            {
                return price * 0.1;
            }
        }

        public class SeniorDiscount : IDiscount
        {
            double IDiscount.Value(double price)
            {
                return price * 0.2;
            }
        }

        public class DefaultDiscount : IDiscount
        {
            double IDiscount.Value(double price)
            {
                return price;
            }
        }
        #endregion

    }


    /// </summary>
    public class Task1_S_Solid
    {


        #region invoice
        /*
         *  Task: Split the Invoice class so that the database and printing operations are segregated into their respective classes.
            public class Invoice
            {
            public double Amount {get; set;}
            public string CustomerName {get; set;}
            // ... other properties
            public void PrintInvoice() 
            {
                // Print invoice
            }
            public void SaveInvoice() 
            {
                // Save invoice to database
            }
            }
         */
        public class Invoice
        {
            public double Amount { get; set; }
            public string CustomerName { get; set; }
        }

        public class PrintInvoice
        {
            public void Print(Invoice item)
            {

            }

        }
        public class Database_Invoice
        {


            public void SaveInvoice(Invoice item)
            {

            }

        }
        //---------------------------------------------------
        #endregion

        #region book
        /*
        Task: Separate the database operations from the Book class, ensuring that each class has a single responsibility.
        public class Book 
        {
            private string Title {get; set;}
            private string Author {get; set;}
            // ... other properties
            public void SaveToDatabase()
            {
                  // Save book to the database
            }
            public string GetBookSummary() 
            {
                  return title + &quot; by &quot; + author;
            }
        }
       */
        public class Book
        {
            private string Title { get; set; }
            private string Author { get; set; }
            // ... other properties

            public string GetBookSummary()
            {
                return $"{this.Title} - {this.Author}";
            }
        }

        public class Database_Book
        {
            public void SaveToDatabase(Book item)
            {
                // Save book to the database
            }

        

        }
        #endregion

    }
}
