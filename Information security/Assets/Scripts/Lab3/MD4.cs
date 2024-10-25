using System;
using System.Text;

namespace Lab3
{
    public class MD4
    {
        private uint[] _state; // Внутреннее состояние (4 регистра по 32 бита)
        private byte[] _buffer; // Буфер для хранения данных в процессе хеширования
        private long _count; // Счетчик количества обработанных байт

        public MD4()
        {
            _state = new uint[4]; // Массив для хранения 4-х 32-битных значений (A, B, C, D)
            _buffer = new byte[64]; // Буфер для хранения 512 бит (64 байта)
            _count = 0; // Счетчик для количества обработанных байтов

            // Инициализация хеш-значений
            _state[0] = 0x67452301; // A
            _state[1] = 0xefcdab89; // B
            _state[2] = 0x98badcfe; // C
            _state[3] = 0x10325476; // D
        }

        // Метод для обработки одного блока данных размером 512 бит (64 байта)
        private void ProcessBlock(byte[] block, int offset)
        {
            // Создание массива для хранения 16 32-битных слов (512 бит = 16 слов по 32 бита)
            uint[] x = new uint[16];

            // Разбиение блока на 16 слов
            for (int i = 0; i < 16; i++)
            {
                // Преобразуем 4 байта из блока в 32-битное слово
                x[i] = BitConverter.ToUInt32(block, offset + i * 4);
            }

            // Сохраняем текущее состояние
            uint a = _state[0];
            uint b = _state[1];
            uint c = _state[2];
            uint d = _state[3];

            // Первый раунд - логические операции с применением функции FF и сдвигами
            FF(ref a, b, c, d, x[0], 3);
            FF(ref d, a, b, c, x[1], 7);
            FF(ref c, d, a, b, x[2], 11);
            FF(ref b, c, d, a, x[3], 19);
            FF(ref a, b, c, d, x[4], 3);
            FF(ref d, a, b, c, x[5], 7);
            FF(ref c, d, a, b, x[6], 11);
            FF(ref b, c, d, a, x[7], 19);
            FF(ref a, b, c, d, x[8], 3);
            FF(ref d, a, b, c, x[9], 7);
            FF(ref c, d, a, b, x[10], 11);
            FF(ref b, c, d, a, x[11], 19);
            FF(ref a, b, c, d, x[12], 3);
            FF(ref d, a, b, c, x[13], 7);
            FF(ref c, d, a, b, x[14], 11);
            FF(ref b, c, d, a, x[15], 19);

            // Второй раунд - операции с использованием функции GG и дополнительной константы
            GG(ref a, b, c, d, x[0], 3);
            GG(ref d, a, b, c, x[4], 5);
            GG(ref c, d, a, b, x[8], 9);
            GG(ref b, c, d, a, x[12], 13);
            GG(ref a, b, c, d, x[1], 3);
            GG(ref d, a, b, c, x[5], 5);
            GG(ref c, d, a, b, x[9], 9);
            GG(ref b, c, d, a, x[13], 13);
            GG(ref a, b, c, d, x[2], 3);
            GG(ref d, a, b, c, x[6], 5);
            GG(ref c, d, a, b, x[10], 9);
            GG(ref b, c, d, a, x[14], 13);
            GG(ref a, b, c, d, x[3], 3);
            GG(ref d, a, b, c, x[7], 5);
            GG(ref c, d, a, b, x[11], 9);
            GG(ref b, c, d, a, x[15], 13);

            // Третий раунд - операции с использованием функции HH и другой константы
            HH(ref a, b, c, d, x[0], 3);
            HH(ref d, a, b, c, x[8], 9);
            HH(ref c, d, a, b, x[4], 11);
            HH(ref b, c, d, a, x[12], 15);
            HH(ref a, b, c, d, x[2], 3);
            HH(ref d, a, b, c, x[10], 9);
            HH(ref c, d, a, b, x[6], 11);
            HH(ref b, c, d, a, x[14], 15);
            HH(ref a, b, c, d, x[1], 3);
            HH(ref d, a, b, c, x[9], 9);
            HH(ref c, d, a, b, x[5], 11);
            HH(ref b, c, d, a, x[13], 15);
            HH(ref a, b, c, d, x[3], 3);
            HH(ref d, a, b, c, x[11], 9);
            HH(ref c, d, a, b, x[7], 11);
            HH(ref b, c, d, a, x[15], 15);

            // Обновление состояния
            _state[0] += a;
            _state[1] += b;
            _state[2] += c;
            _state[3] += d;
        }

        // Логическая функция FF для первого раунда
        private static void FF(ref uint a, uint b, uint c, uint d, uint x, int s)
        {
            // Выполняет операцию F и сдвиг
            a += (b & c) | (~b & d) + x; // Логическая операция
            a = RotateLeft(a, s); // Циклический сдвиг
        }

        // Логическая функция GG для второго раунда
        private static void GG(ref uint a, uint b, uint c, uint d, uint x, int s)
        {
            // Выполняет операцию G и сдвиг
            a += (b & c) | (b & d) | (c & d) + x + 0x5a827999; // Логическая операция с константой
            a = RotateLeft(a, s); // Циклический сдвиг
        }

        // Логическая функция HH для третьего раунда
        private static void HH(ref uint a, uint b, uint c, uint d, uint x, int s)
        {
            // Выполняет операцию H и сдвиг
            a += b ^ c ^ d + x + 0x6ed9eba1; // Логическая операция с константой
            a = RotateLeft(a, s); // Циклический сдвиг
        }

        // Функция циклического сдвига влево
        private static uint RotateLeft(uint x, int n)
        {
            return (x << n) | (x >> (32 - n)); // Сдвигает биты влево и объединяет
        }

        // Метод для обновления состояния с новыми данным
        private void Update(byte[] input)
        {
            int bufferIndex = (int)(_count % 64); // Текущая позиция в буфере
            _count += input.Length; // Увеличиваем счетчик на длину входных данных

            int partLength = 64 - bufferIndex; // Оставшееся место в буфере
            int i = 0; // Индекс для прохода по входным данным

            // Если входные данные превышают оставшееся место в буфере
            if (input.Length >= partLength)
            {
                Array.Copy(input, 0, _buffer, bufferIndex, partLength); // Копируем данные в буфер
                ProcessBlock(_buffer, 0); // Обрабатываем полный блок
                
                // Обрабатываем все оставшиеся полные блоки
                for (i = partLength; i + 63 < input.Length; i += 64) 
                {
                    ProcessBlock(input, i);
                }

                bufferIndex = 0; // Сброс индекса буфера
            }

            // Копируем оставшиеся данные во второй половине буфера
            Array.Copy(input, i, _buffer, bufferIndex, input.Length - i);
        }

        // Финализация хеширования и возврат результата
        private byte[] FinalizeHash()
        {
            byte[] padding = new byte[64]; // Создание массива для дополнения
            padding[0] = 0x80; // Добавление бита 1 в начало

            int bufferIndex = (int)(_count % 64); // Индекс текущего положения в буфере
            int padLength = (bufferIndex < 56) ? (56 - bufferIndex) : (120 - bufferIndex); // Определение длины дополнения

            // Преобразование общего количества бит в массив байтов
            byte[] lengthBytes = BitConverter.GetBytes(_count * 8);

            // Обновление состояния с дополнением и длиной
            Update(padding[..padLength]); // Добавляем дополнение
            Update(lengthBytes); // Добавляем длину

            // Формирование результирующего хеша
            byte[] hash = new byte[16]; // Создание массива для хеш-значения
            for (int i = 0; i < 4; i++)
            {
                // Копируем 4 байта хеша в результирующий массив
                Array.Copy(BitConverter.GetBytes(_state[i]), 0, hash, i * 4, 4);
            }

            return hash; // Возврат итогового хеш-значения
        }

        // Статический метод для получения хеш-значения строки
        public static string Hash(byte[] input)
        {
            MD4 md4 = new MD4(); // Создание нового экземпляра MD4
            md4.Update(input); // Обновление состояния с входными данными
            byte[] hash = md4.FinalizeHash(); // Финализация хеширования

            StringBuilder sb = new StringBuilder(); // Создание StringBuilder для хеш-строки
            foreach (byte b in hash)
            {
                // Преобразование каждого байта в шестнадцатеричную строку
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString(); // Возврат итоговой хеш-строки
        }
    }
}
