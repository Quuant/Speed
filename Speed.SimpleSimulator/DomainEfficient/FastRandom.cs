using System.Runtime.CompilerServices;

namespace Speed.SimpleSimulator.DomainEfficient
{
    public struct FastRandom
    {
        private uint _state;

        public FastRandom(int seed)
        {
            // State must be non-zero
            _state = (uint)seed;
            if (_state == 0) 
                _state = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NextDouble()
        {
            // Xorshift algorithm
            uint x = _state;
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            _state = x;

            // 3. Convert uint to double [0..1)
            // Multiplication is faster than division
            return x * (1.0 / 4294967295.0);
        }
    }
}