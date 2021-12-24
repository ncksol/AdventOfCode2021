using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Puzzle231UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[][] _input = new char[3][];
        private int _totalEnergy = 0;

        public MainWindow()
        {
            InitializeComponent();

            ResetBoard();
        }

        private Button _source;

        private Button _prevSource;
        private Button _prevTarget;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_source == null)
            {
                _source = sender as Button;
                _source.Background = Brushes.Red;
                return;
            }

            var target = sender as Button;

            if (_source.Name.StartsWith("Room"))
            {
                if (target.Name.StartsWith("Hallway") == false) return;

                var str = _source.Name.Replace("Room", String.Empty);
                (int X, int Y) roomCoords = (int.Parse(str[1].ToString()), int.Parse(str[0].ToString()));
                str = target.Name.Replace("Hallway", String.Empty);
                var hallwayCoords = int.Parse(str);

                target.Content = _source.Content;
                _source.Content = ".";

                _totalEnergy += MoveOut(roomCoords, hallwayCoords);
            }
            else
            {
                if (target.Name.StartsWith("Room") == false) return;

                var str = target.Name.Replace("Room", String.Empty);
                (int X, int Y) roomCoords = (int.Parse(str[1].ToString()), int.Parse(str[0].ToString()));
                str = _source.Name.Replace("Hallway", String.Empty);
                var hallwayCoords = int.Parse(str);

                target.Content = _source.Content;
                _source.Content = ".";

                _totalEnergy += MoveIn(roomCoords, hallwayCoords);
            }

            TotalEnergy.Text = _totalEnergy.ToString();

            _prevSource = _source;
            _prevTarget = target;

            _source.Background = Brushes.Transparent;
            _source = null;
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (_source != null)
            {
                _source.Background = Brushes.Transparent;
                _source = null;
            }

            if (_prevSource == null || _prevTarget == null) return;

            var energy = 0;

            var source = _prevTarget;
            var target = _prevSource;
            var creature = Convert.ToChar(source.Content);

            if (target.Name.StartsWith("Room"))
            {
                if (source.Name.StartsWith("Hallway") == false) return;

                var str = target.Name.Replace("Room", String.Empty);
                (int X, int Y) roomCoords = (int.Parse(str[1].ToString()), int.Parse(str[0].ToString()));
                str = source.Name.Replace("Hallway", String.Empty);
                var hallwayCoords = int.Parse(str);
                
                energy = MoveIn(roomCoords, hallwayCoords, creature);
            }
            else
            {
                if (source.Name.StartsWith("Room") == false) return;

                var str = source.Name.Replace("Room", String.Empty);
                (int X, int Y) roomCoords = (int.Parse(str[1].ToString()), int.Parse(str[0].ToString()));
                str = target.Name.Replace("Hallway", String.Empty);
                var hallwayCoords = int.Parse(str);

                energy = MoveOut(roomCoords, hallwayCoords, creature);
            }

            _prevSource.Content = creature;
            _prevTarget.Content = ".";

            _totalEnergy -= energy;

            TotalEnergy.Text = _totalEnergy.ToString();
        }


        int MoveOut((int X, int Y) room, int hallway, char? creature = null)
        {
            var energy = 0;

            if(creature == null)
                creature = _input[room.Y][room.X];
            var moveEnergy = GetMoveEnergy(creature);

            energy += (room.Y + 1) * moveEnergy;
            energy += Math.Abs(RoomToHallwayCoord(room.X) - hallway) * moveEnergy;

            _input[2][hallway] = creature.Value;
            _input[room.Y][room.X] = '.';

            return energy;
        }

        int MoveIn((int X, int Y) room, int hallway, char? creature = null)
        {
            var energy = 0;

            if(creature == null)
                creature = _input[2][hallway];
            var moveEnergy = GetMoveEnergy(creature);

            energy += Math.Abs(RoomToHallwayCoord(room.X) - hallway) * moveEnergy;
            energy += (room.Y + 1) * moveEnergy;

            _input[2][hallway] = '.';
            _input[room.Y][room.X] = creature.Value;

            return energy;
        }

        int GetMoveEnergy(char? creature) =>
            creature switch
            {
                'A' => 1,
                'B' => 10,
                'C' => 100,
                'D' => 1000
            };

        int RoomToHallwayCoord(int room) =>
            room switch
            {
                0 => 2,
                1 => 4,
                2 => 6,
                3 => 8
            };

        int HallwayToRoomCoord(int room) =>
            room switch
            {
                2 => 0,
                4 => 1,
                6 => 2,
                8 => 3
            };

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetBoard();
        }

        private void ResetBoard()
        {
            Hallway0.Content = ".";
            Hallway1.Content = ".";
            Hallway2.Content = ".";
            Hallway3.Content = ".";
            Hallway4.Content = ".";
            Hallway5.Content = ".";
            Hallway6.Content = ".";
            Hallway7.Content = ".";
            Hallway8.Content = ".";
            Hallway9.Content = ".";
            Hallway10.Content = ".";
            /*
                        Room00.Content = "B";
                        Room01.Content = "C";
                        Room02.Content = "B";
                        Room03.Content = "D";

                        Room10.Content = "A";
                        Room11.Content = "D";
                        Room12.Content = "C";
                        Room13.Content = "A";


                        _input[0] = new char[] { 'B', 'C', 'B', 'D' };
                        _input[1] = new char[] { 'A', 'D', 'C', 'A' };
                        _input[2] = new char[11];*/

            Room00.Content = "A";
            Room01.Content = "C";
            Room02.Content = "C";
            Room03.Content = "D";

            Room10.Content = "B";
            Room11.Content = "D";
            Room12.Content = "A";
            Room13.Content = "B";


            _input[0] = new char[] { 'A', 'C', 'C', 'D' };
            _input[1] = new char[] { 'B', 'D', 'A', 'B' };
            _input[2] = new char[11];


            _totalEnergy = 0;
            TotalEnergy.Text = _totalEnergy.ToString();
        }
    }
}
