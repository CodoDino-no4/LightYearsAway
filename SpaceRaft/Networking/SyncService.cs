using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Networking
{
		public class SyncService : BackgroundService
		{
				private ClientManager clientManager;

				public SyncService( ClientManager clientManager )
				{
						this.clientManager = clientManager;
				}

				protected override async Task ExecuteAsync( CancellationToken stoppingToken )
				{
						try
						{
								while (!stoppingToken.IsCancellationRequested)
								{
										clientManager.JoinServer();
										//clientManager.StartLoop();

										await Task.Delay( TimeSpan.FromMinutes( 1 ), stoppingToken );
								}
						}
						catch (TaskCanceledException)
						{
								// When the stopping token is canceled, for example, a call made from services.msc,
								// we shouldn't exit with a non-zero exit code. In other words, this is expected...
						}
						catch (Exception ex)
						{
								Console.WriteLine( ex.Message );

								// Terminates this process and returns an exit code to the operating system.
								// This is required to avoid the 'BackgroundServiceExceptionBehavior', which
								// performs one of two scenarios:
								// 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
								// 2. When set to "StopHost": will cleanly stop the host, and log errors.
								//
								// In order for the Windows Service Management system to leverage configured
								// recovery options, we need to terminate the process with a non-zero exit code.
								Environment.Exit( 1 );
						}
				}
		}
}
