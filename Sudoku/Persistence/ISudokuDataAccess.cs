using System.Threading.Tasks;

namespace ELTE.Forms.Sudoku.Persistence
{
    public interface ISudokuDataAccess
    {
        Task<SudokuTable> Load(string path);

        Task Save(string fileName, SudokuTable path);
    }
}