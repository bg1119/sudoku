using System.IO;
using System.Threading.Tasks;

namespace ELTE.Forms.Sudoku.Persistence
{
    /// <summary>
    /// Sudoku fájlkezelő típusa.
    /// </summary>
    public class SudokuFileDataAccess : ISudokuDataAccess
    {
        /// <summary>
        /// Fájl betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <returns>A fájlból beolvasott játéktábla.</returns>
        public async Task<SudokuTable> Load(string path)
        {
            try
            {
                using (var reader = new StreamReader(path)) // fájl megnyitása
                {
                    var line = await reader.ReadLineAsync();
                    var numbers = line.Split(' '); // beolvasunk egy sort, és a szóköz mentén széttöredezzük
                    var tableSize = int.Parse(numbers[0]); // beolvassuk a tábla méretét
                    var regionSize = int.Parse(numbers[1]); // beolvassuk a házak méretét
                    var table = new SudokuTable(tableSize, regionSize); // létrehozzuk a táblát

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

        /// <summary>
        /// Fájl mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <param name="table">A fájlba kiírandó játéktábla.</param>
        public async Task Save(string path, SudokuTable table)
        {
            try
            {
                using (var writer = new StreamWriter(path)) // fájl megnyitása
                {
                    writer.Write(table.Size); // kiírjuk a méreteket
                    await writer.WriteLineAsync(" " + table.RegionSize);
                    for (var i = 0; i < table.Size; i++)
                    {
                        for (var j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " "); // kiírjuk az értékeket
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