namespace PacMan.SoundProvider
{
    using NAudio.Wave;

    public class LoopStream : WaveStream
    {
        private WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream, bool enableLooping)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = enableLooping;
        }

        public bool EnableLooping { get; set; }

        public override WaveFormat WaveFormat
        {
            get
            {
                return this.sourceStream.WaveFormat;
            }
        }

        public override long Length
        {
            get
            {
                return this.sourceStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.sourceStream.Position;
            }

            set
            {
                this.sourceStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;
            while (totalBytesRead < count)
            {
                int bytesRead = this.sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (this.sourceStream.Position == 0 || !this.EnableLooping)
                    {
                        break;
                    }

                    this.sourceStream.Position = 0;
                }

                totalBytesRead += bytesRead;
            }

            return totalBytesRead;
        }
    }
}