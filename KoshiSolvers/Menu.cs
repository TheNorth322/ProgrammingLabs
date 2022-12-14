using System;
using System.Collections.Generic;
namespace KoshiSolvers
{
    class Menu
    { 
        // Class fields
        private const byte ADD_SOLVER_OPTION = 1;
        private const byte DELETE_SOLVER_OPTION = 2;
        private const byte PRINT_SOLVERS_OPTION = 3;
        private const byte SOLVE_TASK_OPTION = 4;
        private const byte PRINT_SOLUTION_OPTION = 5;
        private const byte EXIT_OPTION = 6;
        
        // Properties
        public Farm Farm { get; }
        public List<List<Point>> Solutions { get; set; }
        
        // Constructors
        public Menu()
        {
            Farm = new Farm();
            Solutions = new List<List<Point>>();
        }
        
        // Methods
        public void Listen()
        {
            while (true)
            {
                Console.Write(
                        $@"
                        Menu options
                        {ADD_SOLVER_OPTION}-Add solver
                        {DELETE_SOLVER_OPTION}-Delete solver
                        {PRINT_SOLVERS_OPTION}-Print solvers
                        {SOLVE_TASK_OPTION}-Solve task
                        {PRINT_SOLUTION_OPTION}-Print solution
                        {EXIT_OPTION}-EXIT"
                        );

                Console.Write("\n\nEnter option: ");
                byte option;
                byte.TryParse(Console.ReadLine(), out option) ;

                switch (option)
                {
                    case 1:
                        HandleAddSolverOption();
                        break;
                    case 2:
                        HandleDeleteSolverOption();
                        break;
                    case 3:
                        HandlePrintSolversOption();
                        break;
                    case 4:
                        HandleSolveTaskOption();
                        break;
                    case 5:
                        HandlePrintSolution();
                        break;
                    case 6:
                        return;
                    default:
                        Console.Write("There is no such option!");
                        break;
                }
            }
        }
        
        private void HandleAddSolverOption()
        {
            try
            {
                Console.Write("Enter solver type (1 - Euler Method, 2 - Hoin Method): ");
                SolverTypes Type = (SolverTypes)Convert.ToByte(Console.ReadLine());

                Console.Write("Enter solver name: ");
                string Name = Console.ReadLine();

                Console.Write("Enter solver behavior(1 - Stop at left border, 2 - after left border, 3 - before left border): ");
                BehaviorOfSolver Behavior = (BehaviorOfSolver)Convert.ToByte(Console.ReadLine());

                switch (Type)
                {
                    case SolverTypes.EulerSolver:
                        Farm.Solvers.Add(new EulerSolver(Name, Behavior));
                        break;
                    case SolverTypes.HoinSolver:
                        Farm.Solvers.Add(new HoinSolver(Name, Behavior));
                        break;
                    default:
                        throw new ArgumentException("Wrong solver type!");
                }
                Console.WriteLine("Solver was successfully added!");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        private void HandleDeleteSolverOption()
        {
            try
            {
                Console.Write("Enter solver name: ");
                string Name = Console.ReadLine();
                Farm.DeleteSolver(Name);
                Console.WriteLine("Solver was successfully deleted");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        private void HandlePrintSolversOption()
        {
            if (Farm.Solvers.Count == 0)
            {
                Console.WriteLine("There are no such solvers yet");
                return;
            }
            foreach (Solver solver in Farm.Solvers)
            {
                Console.WriteLine($"Solver name: {solver.Name}\nSolver behavior: {solver.Behavior}\n");
            }
        }

        private void HandleSolveTaskOption()
        {
            try
            {
                Console.Write("Enter y0: ");
                double y0 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter t0: ");
                double t0 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter t: ");
                double t = Convert.ToDouble(Console.ReadLine());
                
                Console.Write("Enter h: ");
                double h = Convert.ToDouble(Console.ReadLine());
                
                InitialValueProblem task = new InitialValueProblem(y0, t0, t, h);
                
                Farm.SolveTask(task);

                Console.Write("The task was successfully completed!");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        private void HandlePrintSolution()
        {
            try
            {
                Console.Write("Enter solver name: ");
                string name = Console.ReadLine();
                int i = 0, index = Farm.FindSolverByName(name);

                if (Solutions[index].Count == 0)
                    throw new ArgumentException("This solver doesn't have a solution yet!");

                foreach (Point point in Solutions[index]) 
                {
                    Console.WriteLine($"x{i}: {point.X}, y{i}: {point.Y}");
                    i++;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
