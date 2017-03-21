
namespace arrayextensions {
    public static class Arrays {
        public static int[] Add(this int [] a1, int [] a2) {
            var n = new int[a1.Length + a2.Length];
            a1.CopyTo(n, 0);
            a2.CopyTo(n, a1.Length);
            return n;
        }
    }
}