namespace PacMan.SoundProvider.SoundPlayers
{
    using System.IO;
    using NAudio.Wave;
    using PacMan.SoundProvider;

    public abstract class SoundPlayer : ISoundPlayer
    {
        private readonly string startupPath = Directory.GetParent(@"../../").FullName;
        private LoopStream stream;

        protected SoundPlayer(string filePath, bool enableLooping)
        {
            var reader = new WaveFileReader(this.startupPath + filePath);
            this.stream = new LoopStream(reader, enableLooping);
            this.Init();
        }

        protected SoundPlayer(string filePath, bool enableLooping, float volume)
           : this(filePath, enableLooping)
        {
            this.Volume = volume;
        }

        protected IWavePlayer WavePlayer { get; set; }

        private float Volume { get; set; }

        public void Play()
        {
            if (this.WavePlayer.PlaybackState != PlaybackState.Playing)
            {
                this.Init();
                this.WavePlayer.Play();
            }
        }

        public void Pause()
        {
            this.WavePlayer.Pause();
        }

        protected void Init()
        {
            this.WavePlayer = new WaveOut();
            this.stream.Position = 0;
            this.WavePlayer.Init(this.stream);
        }
    }
}