namespace MusicApp
{
    public partial class AppForm : Form
    {
        BindingSource musicBindingSource = new BindingSource();

        public AppForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.findPublisher(textBox1.Text);
            
            dataGridView1.DataSource = musicBindingSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.mostCollaborations();

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.findProducers(textBox1.Text);

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.searchByAlbum(textBox1.Text);

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.findAlbumsOfArtist(textBox1.Text);

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.findAlbumsWithMostSongs();

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.discoverNewReleases();

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.searchByArtistAndProducer(textBox1.Text);

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.findDurationOfAlbum(textBox1.Text);

            dataGridView1.DataSource = musicBindingSource;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MusicDAO musicDAO = new MusicDAO();

            musicBindingSource.DataSource = musicDAO.findLastAlbumReleaseDate(textBox1.Text);

            dataGridView1.DataSource = musicBindingSource;
        }
    }
}