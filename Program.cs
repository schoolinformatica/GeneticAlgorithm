using System;
using Individual;
using arrayextensions;
using binaryconverter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        public static Random r = new Random();
        static void Main(string[] args)
        {
            const int populationSize = 10;
            const int maxIterations = 100;
            const double crossoverRate = 0.8;
            const double mutationRate = 0.3;
            const bool elitism = true;
            
            /* FUNCTIONS TO DEFINE (for each problem):
            Func<Ind> createIndividual;                                 ==> input is nothing, output is a new individual
            Func<Ind,double> computeFitness;                            ==> input is one individual, output is its fitness
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents; ==> input is an array of individuals (population) and an array of corresponding fitnesses, output is a function which (without any input) returns a tuple with two individuals (parents)
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover;           ==> input is a tuple with two individuals (parents), output is a tuple with two individuals (offspring/children)
            Func<Ind, double, Ind> mutation;                            ==> input is one individual and mutation rate, output is the mutated individual
            */
            
            Func<Binary> createIndividual = delegate() { return new Binary(5, r.Next(0, 31));};

            Func<Binary, double> computeFitness = delegate(Binary i) {
                int value = i.ToInt();
                double fitness = -Math.Pow(value, 2) + 7*value;

                return fitness;
            };

            Func<Binary[], double[], Func<Tuple<Binary, Binary>>> selectTwoParents = delegate(Binary[] individuals, double[] fitnesses) {
                var indis = individuals;
                var fit = fitnesses;
                List<int> indexes = new List<int>();
                var totalFitness = fitnesses.Sum();
               
                var index = 0;
                while(indexes.Count < 2) {
                    if(r.Next(0, 100) > (fitnesses[index]/totalFitness) * 100) {
                        indexes.Add(index);
                    }
                    index++;
                    if(index >= fitnesses.Length - 1)
                        index = 0;
                }

                return new Func<Tuple<Binary, Binary>> (() => new Tuple<Binary, Binary>(indis[indexes[0]], indis[indexes[1]]));
            };

            Func<Tuple<Binary, Binary>, Tuple<Binary, Binary>> crossover = delegate(Tuple<Binary, Binary> t) {
                var half = t.Item1.Size/2;
                var parnew1 = t.Item1.GetSubArray(0, half).Add(t.Item2.GetSubArray(half + 1, t.Item2.Size -1));
                var parnew2 = t.Item2.GetSubArray(0, half).Add(t.Item1.GetSubArray(half + 1, t.Item1.Size -1));


                return new Tuple<Binary, Binary>(new Binary(parnew1), new Binary(parnew2));
            };

            Func<Binary, double, Binary> mutation = delegate(Binary i, double d) {
                if(r.Next(0, 100) > (1-d)*100) {
                    i.Mutate(r.Next(0, i.Size - 1));
                }

                return i;
            };

           GeneticAlgorithm<Binary> fakeProblemGA = new GeneticAlgorithm<Binary>(crossoverRate, mutationRate, elitism, populationSize, maxIterations); // CHANGE THE GENERIC TYPE (NOW IT'S INT AS AN EXAMPLE) AND THE PARAMETERS VALUES
           var solution = fakeProblemGA.Run(createIndividual, computeFitness, selectTwoParents, crossover, mutation); 
           Console.WriteLine("Solution: ");
           Console.WriteLine(solution.ToString());
           Console.WriteLine(solution.ToInt());
        //    for(int i = 0; i < 100; i++) {
        //        Console.WriteLine(r.Next(0, 31));
        //    }

            // var ind1 = createIndividual();
            // var ind2 = createIndividual();
            // Ind[] ind = new Ind[5] {
            // new Ind(new int[5] {1, 0, 1, 1, 0}),
            // new Ind(new int[5] {1, 1, 1, 0, 0}),
            // new Ind(new int[5] {1, 0, 0, 1, 0}),
            // new Ind(new int[5] {1, 0, 0, 0, 0}),
            // new Ind(new int[5] {0, 0, 1, 0, 1}),
            // };
            // double[] fitnes = new double[5];

            // for(int i = 0; i < 5; i++) {
            //     fitnes[i] = computeFitness(ind[i]);
            // }           
            // var parents = new Tuple<Ind, Ind>(ind1, ind2);
            // var TwoParents = selectTwoParents(ind, fitnes)();
            
            // Console.WriteLine("create test: " + ind1.ToString() + " ][ " + ind1.ToInt());
            // Console.WriteLine("--------------------------------");
            // Console.WriteLine("Fitness test: " + computeFitness(ind1));
            // Console.WriteLine("--------------------------------");
            // Console.WriteLine("Parent1: " + parents.Item1.ToString() + " _ Parent2: " + parents.Item2.ToString());
            // Console.WriteLine("crossover: new par1: " + crossover(parents).Item1.ToString() + " _ new par2: " + crossover(parents).Item2.ToString() );
            // Console.WriteLine("--------------------------------");
            // Console.WriteLine("Before mutation: " + ind1.ToString() + " After mutation: " + mutation(ind1, 1).ToString());         
            // Console.WriteLine("--------------------------------");
            // Console.WriteLine("Select parents: " +  TwoParents.Item1.ToString() + " " + TwoParents.Item2.ToString());
            // Console.WriteLine("With values: " +  TwoParents.Item1.ToInt() + " " + TwoParents.Item2.ToInt());

            // for(int i = 0; i < 5; i++) {
            //     Console.WriteLine(computeFitness(ind[i]));
            // }    
            

        
        }
    }
}
    