using System;
using System.IO;
using System.Text;

namespace SnakeGame
{
    public static class WavGenerator
    {
        public static void CreateMemeWavFiles()
        {
            // Create sounds directory - FIXED: Using AppDomain instead of Application
            string soundsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");
            Directory.CreateDirectory(soundsDir);

            Console.WriteLine("🔊 Generating WAV files...");

            // Generate all game sounds
            GenerateEatSound(Path.Combine(soundsDir, "eat.wav"));
            GenerateGameOverSound(Path.Combine(soundsDir, "gameover.wav"));
            GenerateLevelUpSound(Path.Combine(soundsDir, "levelup.wav"));
            GenerateBonusSound(Path.Combine(soundsDir, "bonus.wav"));
            GenerateSpeedSound(Path.Combine(soundsDir, "speed.wav"));
            GenerateSlowSound(Path.Combine(soundsDir, "slow.wav"));

            Console.WriteLine("✅ WAV files generated in 'Sounds' folder!");
        }

        private static void GenerateEatSound(string filePath)
        {
            // "Nom nom" sound - two quick beeps
            byte[] soundData = GenerateBeep(800, 100, 0.5f);
            byte[] soundData2 = GenerateBeep(900, 80, 0.5f);

            byte[] combined = new byte[soundData.Length + soundData2.Length];
            Array.Copy(soundData, 0, combined, 0, soundData.Length);
            Array.Copy(soundData2, 0, combined, soundData.Length, soundData2.Length);

            WriteWavFile(filePath, combined, 44100, 1);
        }

        private static void GenerateGameOverSound(string filePath)
        {
            // Sad trombone sound
            byte[] sound1 = GenerateBeep(400, 300, 0.7f);
            byte[] sound2 = GenerateBeep(350, 300, 0.7f);
            byte[] sound3 = GenerateBeep(300, 400, 0.7f);

            byte[] combined = new byte[sound1.Length + sound2.Length + sound3.Length];
            int offset = 0;
            Array.Copy(sound1, 0, combined, offset, sound1.Length);
            offset += sound1.Length;
            Array.Copy(sound2, 0, combined, offset, sound2.Length);
            offset += sound2.Length;
            Array.Copy(sound3, 0, combined, offset, sound3.Length);

            WriteWavFile(filePath, combined, 44100, 1);
        }

        private static void GenerateLevelUpSound(string filePath)
        {
            // Mario coin sound
            byte[] sound1 = GenerateBeep(523, 150, 0.8f);
            byte[] sound2 = GenerateBeep(659, 150, 0.8f);
            byte[] sound3 = GenerateBeep(784, 200, 0.8f);

            byte[] combined = new byte[sound1.Length + sound2.Length + sound3.Length];
            int offset = 0;
            Array.Copy(sound1, 0, combined, offset, sound1.Length);
            offset += sound1.Length;
            Array.Copy(sound2, 0, combined, offset, sound2.Length);
            offset += sound2.Length;
            Array.Copy(sound3, 0, combined, offset, sound3.Length);

            WriteWavFile(filePath, combined, 44100, 1);
        }

        private static void GenerateBonusSound(string filePath)
        {
            // Exciting ding sound
            byte[] sound1 = GenerateBeep(784, 100, 0.8f);
            byte[] sound2 = GenerateBeep(880, 100, 0.8f);
            byte[] sound3 = GenerateBeep(988, 200, 0.8f);

            byte[] combined = new byte[sound1.Length + sound2.Length + sound3.Length];
            int offset = 0;
            Array.Copy(sound1, 0, combined, offset, sound1.Length);
            offset += sound1.Length;
            Array.Copy(sound2, 0, combined, offset, sound2.Length);
            offset += sound2.Length;
            Array.Copy(sound3, 0, combined, offset, sound3.Length);

            WriteWavFile(filePath, combined, 44100, 1);
        }

        private static void GenerateSpeedSound(string filePath)
        {
            // Zoom sound
            byte[] sound1 = GenerateBeep(1000, 100, 0.7f);
            byte[] sound2 = GenerateBeep(1200, 100, 0.7f);
            byte[] sound3 = GenerateBeep(1400, 100, 0.7f);

            byte[] combined = new byte[sound1.Length + sound2.Length + sound3.Length];
            int offset = 0;
            Array.Copy(sound1, 0, combined, offset, sound1.Length);
            offset += sound1.Length;
            Array.Copy(sound2, 0, combined, offset, sound2.Length);
            offset += sound2.Length;
            Array.Copy(sound3, 0, combined, offset, sound3.Length);

            WriteWavFile(filePath, combined, 44100, 1);
        }

        private static void GenerateSlowSound(string filePath)
        {
            // Slow motion sound
            byte[] sound1 = GenerateBeep(400, 200, 0.6f);
            byte[] sound2 = GenerateBeep(350, 200, 0.6f);
            byte[] sound3 = GenerateBeep(300, 200, 0.6f);

            byte[] combined = new byte[sound1.Length + sound2.Length + sound3.Length];
            int offset = 0;
            Array.Copy(sound1, 0, combined, offset, sound1.Length);
            offset += sound1.Length;
            Array.Copy(sound2, 0, combined, offset, sound2.Length);
            offset += sound2.Length;
            Array.Copy(sound3, 0, combined, offset, sound3.Length);

            WriteWavFile(filePath, combined, 44100, 1);
        }

        private static byte[] GenerateBeep(double frequency, int durationMs, float volume)
        {
            int sampleRate = 44100;
            int amplitude = (int)(32767 * volume);
            double angularFrequency = 2 * Math.PI * frequency;

            int samples = (int)(sampleRate * durationMs / 1000.0);
            byte[] data = new byte[samples * 2]; // 16-bit audio

            for (int i = 0; i < samples; i++)
            {
                double time = (double)i / sampleRate;
                short value = (short)(amplitude * Math.Sin(angularFrequency * time));

                // Convert to little-endian bytes
                data[i * 2] = (byte)(value & 0xFF);
                data[i * 2 + 1] = (byte)((value >> 8) & 0xFF);
            }

            return data;
        }

        private static void WriteWavFile(string filePath, byte[] data, int sampleRate, int channels)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // RIFF header
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(36 + data.Length);
                writer.Write(Encoding.ASCII.GetBytes("WAVE"));

                // fmt chunk
                writer.Write(Encoding.ASCII.GetBytes("fmt "));
                writer.Write(16); // Chunk size
                writer.Write((short)1); // PCM format
                writer.Write((short)channels);
                writer.Write(sampleRate);
                writer.Write(sampleRate * channels * 2); // Byte rate
                writer.Write((short)(channels * 2)); // Block align
                writer.Write((short)16); // Bits per sample

                // data chunk
                writer.Write(Encoding.ASCII.GetBytes("data"));
                writer.Write(data.Length);
                writer.Write(data);
            }
        }
    }
}