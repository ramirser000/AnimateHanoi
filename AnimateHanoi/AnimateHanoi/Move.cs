
namespace AnimateHanoi
{
    class Move
    {
        private int source, destination;

        public int Source
        {
            get { return source; }
            set { source = value; }
        }

        public int Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public string sourceString()
        {
            int str = source + 1;
            return str.ToString();
        }

        public string destinationString()
        {
            int str = destination + 1;
            return str.ToString();
        }
    }
}
