using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PipesList> lstPipeTypes = new List<PipesList>();

            lstPipeTypes.Add(new PipesList("PVC Pipes"));
            lstPipeTypes[0].Pipes.Add(new Pipe("The blue pipe", 12));
            lstPipeTypes[0].Pipes.Add(new Pipe("The red pipe", 15));
            lstPipeTypes[0].Pipes.Add(new Pipe("The silver pipe", 6));
            lstPipeTypes[0].Pipes.Add(new Pipe("The green pipe", 52));

            lstPipeTypes.Add(new PipesList("Iron Pipes"));
            lstPipeTypes[1].Pipes.Add(new Pipe("The gold pipe", 9));
            lstPipeTypes[1].Pipes.Add(new Pipe("The orange pipe", 115));
            lstPipeTypes[1].Pipes.Add(new Pipe("The pink pipe", 1));

            lstPipeTypes.Add(new PipesList("Chrome Pipes"));
            lstPipeTypes[2].Pipes.Add(new Pipe("The grey pipe", 12));
            lstPipeTypes[2].Pipes.Add(new Pipe("The black pipe", 15));
            lstPipeTypes[2].Pipes.Add(new Pipe("The white pipe", 19));
            lstPipeTypes[2].Pipes.Add(new Pipe("The brown pipe", 60));
            lstPipeTypes[2].Pipes.Add(new Pipe("The peach pipe", 16));


            lstPipeTypes = RemoveTheSmallPipes(lstPipeTypes);

            foreach (var pipeList in lstPipeTypes)
            {
                Console.WriteLine("PipesList: {0}", pipeList.pipeType);

                foreach (var pipe in pipeList.Pipes)
                {
                    Console.WriteLine("{0}, length: {1}", pipe.name, pipe.length);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Done, press return to exit");
            Console.ReadLine();
        }


        public static List<PipesList> RemoveTheSmallPipes(List<PipesList> lstPipeTypes)
        {

            //Place your code in here
            //It should remove all pipes that have lengths lower than 19.

            return lstPipeTypes;

        }
    }

    class PipesList
    {
        public string pipeType;
        public List<Pipe> Pipes;

        public PipesList(string newBoxType)
        {
            pipeType = newBoxType;
            Pipes = new List<Pipe>();
        }
    }

    class Pipe
    {
        public string name;
        public float length;

        public Pipe(string newName, float newLength)
        {
            this.name = newName;
            this.length = newLength;
        }
    }


}
