using System.IO;
using System.Threading.Tasks;

namespace ELTE.Forms.Sudoku.Persistence
{
    public class SudokuFileDataAccess : ISudokuDataAccess
    {
        public async Task<SudokuTable> Load(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var line = await reader.ReadLineAsync();
                    var numbers = line.Split(' ');
                    var tableSize = int.Parse(numbers[0]);
                    var regionSize = int.Parse(numbers[1]);
                    var table = new SudokuTable(tableSize, regionSize);

                    for (var i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');
                        for (var j = 0; j < tableSize; j++)
                        {
                            table.SetValue(i, j, int.Parse(numbers[j]), numbers[j] != "0");
                        }
                    }

                    return table;
                }
            }
            catch
            {
                throw new SudokuDataException();
            }
        }

        public async Task Save(string path, SudokuTable table)
        {
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(table.Size);
                    await writer.WriteLineAsync(" " + table.RegionSize);
                    for (var i = 0; i < table.Size; i++)
                    {
                        for (var j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new SudokuDataException();
            }
        }
    }
}