﻿namespace KaiZyo {
    internal class Program {
        static void Main(string[] args) {
            int atai=10,x=0,y=1;
            while (x < atai) {
                y*=++x;
            }
            Console.WriteLine("{0}! = {1:N0}",x,y);
        }
    }
}