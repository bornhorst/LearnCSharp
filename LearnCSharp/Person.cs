using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCSharp
{
    // create a StringProcessor delegate type
    delegate void StringProcessor(string input);

    class Person
    {
        string name;
        DateTime birth;
        DateTime? death;    // nullable type
        public Person(string name, DateTime birth, DateTime? death)
        {
            this.name = name;
            this.birth = birth;
            this.death = death;
        }
        public TimeSpan Age
        {
            get
            {
                DateTime lastAlive = (death == null ? DateTime.Now : death.Value);
                return lastAlive - birth;
            }
        }
        public void Say(string message)
        {
            Console.WriteLine("{0} says: {1}", name, message);
        }
    }
    class Background
    {
        public static void Note(string note)
        {
            Console.WriteLine("({0})", note);
        }
    }
}
