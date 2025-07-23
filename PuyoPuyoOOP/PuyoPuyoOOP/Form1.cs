using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PuyoPuyo.Form1.operation;
using static System.Net.Mime.MediaTypeNames;

namespace PuyoPuyo
{
    public partial class Form1 : Form
    {
        // 1. 인터페이스 설계
        public interface IPuyoBoard<T>
        {
            T[,] Field { get; }
            void InitField();
            void PrintField(TextBox tb);
            bool IsInside(int row, int col);
        }

        public interface Ioperation<T> where T : struct
        {
            void CreatePuyos();
            void MoveLeft();
            void MoveRight();
            bool MoveDown();
            void Rotate();
        }

        // 2. 커스텀 예외 정의
        public class PuyoException : ApplicationException
        {
            public PuyoException(string msg) : base(msg) { }
        }

        // 3. 구현 클래스
        public class PuyoBoard : IPuyoBoard<int>
        {
            public const int FIELD_ROW = 10, FIELD_COL = 6, FINISH_LINE = 3;
            public int[,] Field { get; private set; }

            public PuyoBoard()
            {
                Field = new int[FIELD_ROW, FIELD_COL];
                InitField();
            }

            public void InitField()
            {
                for (int i = 0; i < FIELD_ROW; i++)
                    for (int j = 0; j < FIELD_COL; j++)
                        Field[i, j] = 0;
            }

            public bool IsInside(int row, int col) => row >= 0 && row < FIELD_ROW && col >= 0 && col < FIELD_COL;

            public void PrintField(TextBox tb)
            {
                if (tb == null) throw new ArgumentNullException(nameof(tb));

                tb.Clear();
                for (int i = 0; i < FIELD_ROW; i++)
                {
                    for (int j = 0; j < FIELD_COL; j++)
                    {
                        tb.Text += Field[i, j] > 0 ? Field[i, j].ToString() : " ";
                    }
                    tb.Text += Environment.NewLine;
                }
            }
        }

        public class operation : Ioperation<int>
        {
            private readonly IPuyoBoard<int> board;
            private static readonly int MIN_PUYO = 1, MAX_PUYO = 3;
            private struct PuyoPair { public int value1, i1, j1, dropI1, dropJ1, value2, i2, j2, dropI2, dropJ2; }
            private PuyoPair currentPair;

            public operation(IPuyoBoard<int> board) => this.board = board;

            public void CreatePuyos()
            {
                Random rand = new Random();
                int p1 = rand.Next(MIN_PUYO, MAX_PUYO + 1);
                int p2 = rand.Next(MIN_PUYO, MAX_PUYO + 1);

                currentPair = new PuyoPair { value1 = p1, i1 = 1, j1 = 2, value2 = p2, i2 = 1, j2 = 3 };

                if (!board.IsInside(currentPair.i1, currentPair.j1) || !board.IsInside(currentPair.i2, currentPair.j2))
                    throw new PuyoException("잘못된 위치에 생성되었습니다");

                board.Field[currentPair.i1, currentPair.j1] = currentPair.value1;
                board.Field[currentPair.i2, currentPair.j2] = currentPair.value2;
            }

            public void MoveLeft()
            {
                if (currentPair.j1 > 0 && currentPair.j2 > 0)
                {
                    board.Field[currentPair.i1, currentPair.j1] = 0;
                    board.Field[currentPair.i2, currentPair.j2] = 0;
                    currentPair.j1--; currentPair.j2--;
                    board.Field[currentPair.i1, currentPair.j1] = currentPair.value1;
                    board.Field[currentPair.i2, currentPair.j2] = currentPair.value2;
                }
                else throw new PuyoException("왼쪽으로 더 움직일 수 없습니다");
            }

            public void MoveRight()
            {
                if (currentPair.j1 < PuyoBoard.FIELD_COL - 1 && currentPair.j2 < PuyoBoard.FIELD_COL - 1)
                {
                    board.Field[currentPair.i1, currentPair.j1] = 0;
                    board.Field[currentPair.i2, currentPair.j2] = 0;
                    currentPair.j1++; currentPair.j2++;
                    board.Field[currentPair.i1, currentPair.j1] = currentPair.value1;
                    board.Field[currentPair.i2, currentPair.j2] = currentPair.value2;
                }
                else throw new PuyoException("오른쪽으로 더 움직일 수 없습니다");
            }

            private bool IsVertical() => currentPair.j1 == currentPair.j2 ? true : false;

            private void FindDropRow(out int row, int _col)
            {
                row = -1;

                for (int _row = PuyoBoard.FIELD_ROW - 1; _row >= PuyoBoard.FINISH_LINE; _row--)
                    if (board.Field[_row, _col] == 0)
                    {
                        row = _row;
                        break;
                    }
            }

            private void FindDropRows(out int row1, int _col1, out int row2, int _col2)
            {
                row1 = -1;
                row2 = -1;

                for (int _row = PuyoBoard.FIELD_ROW - 1; _row >= PuyoBoard.FINISH_LINE; _row--)
                {
                    if (board.Field[_row, _col1] <= 0 && row1 < 0)
                    {
                        row1 = _row;
                    }
                    if (board.Field[_row, +_col2] <= 0 && row2 < 0)
                    {
                        row2 = _row;
                    }
                    if (row1 >= 0 && row2 >= 0)
                    {
                        break;
                    }
                }
            }

            public bool MoveDown()
            {
                int index1 = -1, index2 = -1;

                try
                {
                    if (IsVertical())
                    {
                        FindDropRow(out index1, currentPair.j1);
                        if (index1 <= PuyoBoard.FINISH_LINE)
                        {
                            throw new PuyoException("거기에는 놓을 수 없습니다");
                        }

                        index2 = index1 - 1;
                        if (currentPair.i1 > currentPair.i2)
                        {
                            board.Field[index1, currentPair.j1] = currentPair.value1;
                            board.Field[index2, currentPair.j2] = currentPair.value2;
                            currentPair.dropI1 = index1;
                            currentPair.dropI2 = index2;
                        }
                        else
                        {
                            board.Field[index1, currentPair.j1] = currentPair.value2;
                            board.Field[index2, currentPair.j2] = currentPair.value1;
                            currentPair.dropI1 = index2;
                            currentPair.dropI2 = index1;
                        }
                    }
                    else
                    {
                        FindDropRows(out index1, currentPair.j1, out index2, currentPair.j2);
                        if (index1 < PuyoBoard.FINISH_LINE || index2 < PuyoBoard.FINISH_LINE)
                        {
                            throw new PuyoException("거기에는 놓을 수 없습니다");
                        }

                        board.Field[index1, currentPair.j1] = currentPair.value1;
                        board.Field[index2, currentPair.j2] = currentPair.value2;
                        currentPair.dropI1 = index1;
                        currentPair.dropI2 = index2;
                    }
                    currentPair.dropJ1 = currentPair.j1;
                    currentPair.dropJ2 = currentPair.j2;
                    board.Field[currentPair.i1, currentPair.j1] = 0;
                    board.Field[currentPair.i2, currentPair.j2] = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

                return true;
            }

            public void Rotate()
            {
                // 간단 예시 (구체 로직은 상황에 맞게 개선)
                if (currentPair.i1 == currentPair.i2 && currentPair.j1 < currentPair.j2)
                {
                    board.Field[currentPair.i2 + 1, currentPair.j2 - 1] = currentPair.value2;
                    board.Field[currentPair.i2, currentPair.j2] = 0;
                    currentPair.i2++;
                    currentPair.j2--;
                }
                else if (currentPair.i1 == currentPair.i2 && currentPair.j1 > currentPair.j2)
                {
                    board.Field[currentPair.i2 - 1, currentPair.j2 + 1] = currentPair.value2;
                    board.Field[currentPair.i2, currentPair.j2] = 0;
                    currentPair.i2--;
                    currentPair.j2++;
                }
                else if (currentPair.j1 == currentPair.j2 && currentPair.i2 > currentPair.i1)
                {
                    if (currentPair.j2 - 1 >= 0)
                    {
                        board.Field[currentPair.i2 - 1, currentPair.j2 - 1] = currentPair.value2;
                        board.Field[currentPair.i2, currentPair.j2] = 0;
                        currentPair.i2--;
                        currentPair.j2--;
                    }
                    else
                    {
                        throw new PuyoException("여기에서는 회전할 수 없습니다");
                    }
                }
                else if (currentPair.j1 == currentPair.j2 && currentPair.i1 > currentPair.i2)
                {
                    if (currentPair.j2 + 1 < PuyoBoard.FIELD_COL)
                    {
                        board.Field[currentPair.i2 + 1, currentPair.j2 + 1] = currentPair.value2;
                        board.Field[currentPair.i2, currentPair.j2] = 0;
                        currentPair.i2++;
                        currentPair.j2++;
                    }
                    else
                    {
                        throw new PuyoException("여기에서는 회전할 수 없습니다");
                    }
                }
            }

            public void GetDropPoint(out int i1, out int j1, out int i2, out int j2)
            {
                i1 = currentPair.dropI1;
                j1 = currentPair.dropJ1;
                i2 = currentPair.dropI2;
                j2 = currentPair.dropJ2;
            }

            public bool IsGameOver()
            {
                for (int i = 0; i < PuyoBoard.FIELD_COL; i++)
                {
                    if (board.Field[PuyoBoard.FINISH_LINE, i] <= 0 && board.Field[PuyoBoard.FINISH_LINE + 1, i] <= 0)
                    {
                        return false;
                    }
                }
                for (int i = 0; i < PuyoBoard.FIELD_COL - 1; i++)
                {
                    if (board.Field[PuyoBoard.FINISH_LINE, i] <= 0 && board.Field[PuyoBoard.FINISH_LINE, i + 1] <= 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Evaporate(List<(int, int)> _connected)
            {
                //MessageBox.Show($"count {_connected.Count}");
                foreach (var (i, j) in _connected)
                {
                    //MessageBox.Show($"{i},{j}");
                    board.Field[i, j] = 0;
                }
            }

            public void PullDown()
            {
                for (int i = 0; i < PuyoBoard.FIELD_COL; i++)
                {
                    for (int j = PuyoBoard.FIELD_ROW - 1; j >= PuyoBoard.FINISH_LINE + 1; j--)
                    {
                        //MessageBox.Show($"{i},{j}");
                        if (board.Field[j, i] == 0)
                        {
                            int posJ = j;
                            for (int k = posJ - 1; k >= PuyoBoard.FINISH_LINE; k--)
                            {
                                if (board.Field[k, i] > 0)
                                {
                                    board.Field[posJ, i] = board.Field[k, i];
                                    board.Field[k, i] = 0;
                                    posJ--;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        public class IndexFinder<T> where T : struct
        {
            public List<(int, int)> FindConnectedIndices(T[,] arr, int i, int j)
            {
                int rowCount = arr.GetLength(0);
                int colCount = arr.GetLength(1);
                T targetValue = arr[i, j];
                if (targetValue.Equals(default(T))) return new List<(int, int)>();
                bool[,] visited = new bool[rowCount, colCount];
                var result = new List<(int, int)>();
                var queue = new Queue<(int, int)>();
                int[] dx = { -1, 1, 0, 0 }, dy = { 0, 0, -1, 1 };

                queue.Enqueue((i, j)); visited[i, j] = true;
                while (queue.Count > 0)
                {
                    var (x, y) = queue.Dequeue();
                    result.Add((x, y));
                    for (int d = 0; d < 4; d++)
                    {
                        int nx = x + dx[d], ny = y + dy[d];
                        if (nx >= 0 && nx < rowCount && ny >= 0 && ny < colCount)
                        {
                            if (!visited[nx, ny] && arr[nx, ny].Equals(targetValue))
                            {
                                queue.Enqueue((nx, ny)); visited[nx, ny] = true;
                            }
                        }
                    }
                }
                return result;
            }
        }

        private PuyoBoard board;
        private operation op;

        public Form1()
        {
            InitializeComponent();

            board = new PuyoBoard();
            op = new operation(board);

            board.InitField();
            try
            {
                op.CreatePuyos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            board.PrintField(textBox1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                op.MoveLeft();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            board.PrintField(textBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                op.MoveRight();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            board.PrintField(textBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                op.Rotate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            board.PrintField(textBox1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (op.MoveDown())
            {
                board.PrintField(textBox1);
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                if (op.IsGameOver())
                {
                    MessageBox.Show("Game Over");
                }
                else
                {
                    bool evaporated = false;

                    int i1, j1, i2, j2;
                    op.GetDropPoint(out i1, out j1, out i2, out j2);
                    IndexFinder<int> indices = new IndexFinder<int>();
                    List<(int,int)> connected1 = indices.FindConnectedIndices(board.Field, i1, j1);
                    if (connected1.Count >= 4)
                    {
                        op.Evaporate(connected1);
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(500);
                        evaporated = true;
                    }
                    List<(int, int)> connected2 = indices.FindConnectedIndices(board.Field, i2, j2);
                    if (connected2.Count >= 4)
                    {
                        op.Evaporate(connected2);
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(500);
                        evaporated = true;
                    }

                    if (evaporated)
                    {
                        board.PrintField(textBox1);
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(200);
                        op.PullDown();
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(200);
                        board.PrintField(textBox1);
                    }

                    op.CreatePuyos();
                    board.PrintField(textBox1);
                }
            }
        }
    }
}
