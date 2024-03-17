namespace GBCore.Truss
{
    public class Member
    {
        private readonly int _startPointIndex;
        private readonly int _endPointIndex;
        public Member(int startIndex, int endIndex)
        {
            _startPointIndex = startIndex;
            _endPointIndex = endIndex;
        }

    }
}
