using System.ComponentModel;

namespace LYA.Networking
{
		public class SyncService
		{
				private ClientManager clientManager;
				private PacketFormer packetSent;
				private PacketFormer packetRecv;

				private byte[] buffer;
				private string clientData;
				public bool isInit = false;

				private Thread thread;
				public BackgroundWorker worker;

				public SyncService( ClientManager clientManager )
				{
						this.clientManager=clientManager;
						worker=new BackgroundWorker();
				}

				public void InitializeWorker()
				{
						worker.DoWork+=
								new DoWorkEventHandler( Worker_DoWork );

						worker.RunWorkerCompleted+=
								new RunWorkerCompletedEventHandler(
						Worker_RunWorkerCompleted );

						worker.ProgressChanged+=
								new ProgressChangedEventHandler(
						Worker_ProgressChanged );

				}

				public void Worker_DoWork( object sender, DoWorkEventArgs e )
				{
						clientManager.JoinServer();
						//clientManager.MessageLoop();

						//Check if there is a request to cancel the process
						if (worker.CancellationPending)
						{
								e.Cancel=true;
								worker.ReportProgress( 0 );
								return;
						}
				}
				public void Worker_ProgressChanged( object sender, ProgressChangedEventArgs e )
				{

				}

				public void Worker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
				{
						if (e.Cancelled)
						{
								Console.WriteLine( "Process was cancelled" );
						}
						else if (e.Error!=null)
						{
								Console.WriteLine( "There was an error running the process. The thread aborted" );
						}
						else
						{
								Console.WriteLine( "Process was completed" );
						}
				}

		}
}
