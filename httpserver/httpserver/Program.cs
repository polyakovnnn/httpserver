using System;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace httpserver
{
	class Server
	{
		TcpListener Listener; // Объект, принимающий TCP-клиентов


		// Запуск сервера
		public Server(int Port)
		{
			// Создаем "слушателя" для указанного порта
			Listener = new TcpListener(IPAddress.Any, Port);
			Listener.Start(); // Запускаем его

			// В бесконечном цикле
			while (true)
			{
				// Принимаем новых клиентов
				new Client(Listener.AcceptTcpClient());
			}
		}

		// Остановка сервера
		~Server()
		{
			// Если "слушатель" был создан
			if (Listener != null)
			{
				// Остановим его
				Listener.Stop();
			}
		}

		static void Main(string[] args)
		{
			// Создадим новый сервер на порту 80
			new Server(80);
		}
	}

	class Client
	{
		// Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
		public Client(TcpClient Client)

		{
			// Код простой HTML-странички
			string Html = "tema";
			// Необходимые заголовки: ответ сервера, тип и длина содержимого. После двух пустых строк - само содержимое
			string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
			// Приведем строку к виду массива байт
			byte[] Buffer = Encoding.ASCII.GetBytes(Str);
			// Отправим его клиенту
			Client.GetStream().Write(Buffer,  0, Buffer.Length);
			// Закроем соединение
			Client.Close();
		}
	}
}
