using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace AnimateHanoi
{

    class Hanoi
    {
        private MainWindow mainWindow;
        private Canvas canvas;
        private Queue<Move> moves;
        private Stack<Disc>[] pegs;
        private int numOfDisks;
        private readonly double[] PEGXCOORDINATES = {50,190,330};
        private readonly int PEGHEIGHT = 20;
        private bool executingMove;
        private Move currentMove;
        private readonly int PEGONE = 0, PEGTWO = 1, PEGTHREE = 2;
        private int previousPoint = 1;
        private Point firstPoint, secondPoint, thirdPoint, fourthPoint;
        private Disc movingDisc;
        private bool done = false;

        public Hanoi(Canvas canvas, MainWindow mainWindow, int numOfDisks)
        {
            this.numOfDisks = numOfDisks;
            this.mainWindow = mainWindow;
            this.canvas = canvas;

            //Contains move instructions
            moves = new Queue<Move>();

            pegs = new Stack<Disc>[3];
            for(int i = 0; i < 3; i++)
            {
                pegs[i] = new Stack<Disc>();
            }
      
            ColorPicker colorPicker = new ColorPicker();
            //Creating the disks.
            for(int i = 0; i < numOfDisks; i++)
            {
                double newDiskWidth = 80 / (i + 1);            
                Disc newDisk = new Disc(newDiskWidth, PEGHEIGHT, mainWindow, canvas, colorPicker.getColor());
                newDisk.update(PEGXCOORDINATES[PEGONE] - (newDiskWidth/2), (PEGHEIGHT * (numOfDisks - i)));
                pegs[PEGONE].Push(newDisk);
            }

            runHanoi();

            mainWindow.expectedMoves.Content = "Expected moves: " + moves.Count;
            executingMove = false;

        }

        private void runHanoi()
        {
            hinoi(numOfDisks, 0, 1, 2);
        }

        public void draw()
        {          
            for(int i = 0; i < 3; i++)
            {
                foreach (Disc d in pegs[i])
                {
                    d.draw();
                }
            }

        }

        public void update()
        {
            
            //While disc is in motion.
           if(executingMove && !done)
            {
                double orientation = (currentMove.Source < currentMove.Destination) ? 1 : -1;
                                           
                switch(previousPoint)
                {
                    case 1:
                        if(movingDisc.Y != secondPoint.Y)
                        {
                            movingDisc.update(movingDisc.X, movingDisc.Y - 1);
                        }else
                        {
                            previousPoint = 2;
                        }
                        
                        break;
                    case 2:
                        if (movingDisc.X != thirdPoint.X)
                        {
                            movingDisc.update(movingDisc.X + orientation, movingDisc.Y);
                        }
                        else
                        {
                            previousPoint = 3;
                        }
                        break;
                    case 3:
                        if (movingDisc.Y != fourthPoint.Y)
                        {
 
                            movingDisc.update(movingDisc.X, movingDisc.Y + 1);
                            
                        }
                        else
                        {
                            previousPoint = 4;
                        }
                        break;
                    case 4:

                        pegs[currentMove.Destination].Push(pegs[currentMove.Source].Pop());
                        executingMove = false;
                        previousPoint = 1;
                        break;
                    

                }

            }
            else
            {
                if(moves.Count > 0)
                {
                    currentMove = moves.Dequeue();
                    movingDisc = pegs[currentMove.Source].Peek();
                    executingMove = true;

                    firstPoint = new Point(movingDisc.X, movingDisc.Y);
                    double secondPointY = 0;
                    
                    for(int i = numOfDisks; i > 0 && pegs[currentMove.Source].Count != i; i--)
                    {
                        secondPointY++;                                          
                    }
                    secondPoint = new Point(movingDisc.X, movingDisc.Y - (PEGHEIGHT * secondPointY));
                    thirdPoint = new Point(PEGXCOORDINATES[currentMove.Destination] - movingDisc.Width / 2, secondPoint.Y);
                    fourthPoint = new Point(PEGXCOORDINATES[currentMove.Destination] - movingDisc.Width / 2, (PEGHEIGHT* numOfDisks) - (pegs[currentMove.Destination].Count * PEGHEIGHT));

                    mainWindow.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ListBoxItem listBoxItem = new ListBoxItem();
                        listBoxItem.Content = "Source: " + currentMove.sourceString() + " Destination: " + currentMove.destinationString();

                        mainWindow.output.Items.Add(listBoxItem);
                        mainWindow.output.SelectedIndex = mainWindow.output.Items.Count - 1;
                        mainWindow.output.ScrollIntoView(mainWindow.output.SelectedItem);

                    }));
                }
                else
                {
                    done = true;
                }
                
            }
            
            

            

        }

        private void hinoi(int numDisks, int source, int destination, int intermediate)
        {
            if (numDisks == 1)
            {
                moves.Enqueue(createNewMove(source, destination));
            }
            else
            {

                hinoi(numDisks - 1, source, intermediate, destination);               
                moves.Enqueue(createNewMove(source, destination));
                hinoi(numDisks - 1, intermediate, destination, source);
            }



        }

        private Move createNewMove(int source, int destination)
        {
            Move move = new Move();
            move.Source = source;
            move.Destination = destination;

            return move;
        }

    }
}
