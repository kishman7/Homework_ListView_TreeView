using System.IO;

namespace Homework_ListView_TreeView
{
    public static class Helpers
    {
        public static void Rename(this FileInfo file, string newPath)
        {
            file.MoveTo(newPath);
        }

        public static int[] SwapFirstWithLast(this int[] array)
        {
            var tmp = array[0];
            array[0] = array[array.Length - 1];
            array[array.Length - 1] = tmp;

            return array;
        }
    }
}
