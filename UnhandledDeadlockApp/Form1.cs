using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;

namespace UnhandledDeadlockApp
{
    public partial class Form1 : Form
    {
        private readonly Binder _binder = new Binder();

        public SynchronizationContext SynchronizationContext;

        public Form1()
        {
            InitializeComponent();

            SynchronizationContext = SynchronizationContext.Current;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromTicks(2)).
                Subscribe(n => SynchronizationContext.Post(a => _binder.UpdateGrid(), null));

            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(n => _binder.EndUpdate());
        }
    }
}
