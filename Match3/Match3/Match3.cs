namespace Match3;

using global::Match3.Logic;
using global::Match3.Model;

public partial class Match3 : Form, IMatch3View
{
    private readonly Match3Presenter presenter;
    private int timeLeft;

    public Match3()
    {
        presenter = new Match3Presenter(this);
        InitializeComponent();
        InitializeEvents();
    }

    public void ShowStartMenu()
    {
        this.btnStart.Show();
        this.panel2.Show();
    }

    public void HideStartMenu()
    {
        this.btnStart.Hide();
        this.panel2.Hide();
    }

    public void ShowGameField()
    {
        this.pnlGameOver.Visible = false;
        pnlGameOver.Update();

        this.panel1.Show();
        this.tableLayoutPanel1.Show();
        this.tableLayoutPanel1.Visible = true;
        this.label1.Show();
        this.label3.Show();
        this.lblScore.Show();
        this.lblTimeOut.Show();
        panel1.Update();
    }

    public void HideGameField()
    {
        this.pnlGameOver.Visible = false;
        pnlGameOver.Update();
            
        this.panel1.Hide();
        this.tableLayoutPanel1.Hide();
        this.tableLayoutPanel1.Visible = false;
        this.label1.Hide();
        this.label3.Hide();
        this.lblScore.Hide();
        this.lblTimeOut.Hide();
        this.panel1.Update();
    }

    public async Task IncreaseScore(int scorePerIcon)
    {
        this.lblScore.Text = (Convert.ToInt32(this.lblScore.Text) + scorePerIcon).ToString();

        await WaitSomeTime(5);
        this.lblScore.Update();
    }

    public void ResetScore()
    {
        this.lblScore.Text = "0";
        this.lblScore.Update();
    }

    public void ResetDesc()
    {
        lbl11.Text = null;
        lbl12.Text=null;
        lbl13.Text=null;
        lbl14.Text=null;
        lbl15.Text=null;
        lbl16.Text=null;
        lbl17.Text=null;
        lbl18.Text=null;
        lbl21.Text=null;
        lbl22.Text=null;
        lbl23.Text=null;
        lbl24.Text=null;
        lbl25.Text=null;
        lbl26.Text=null;
        lbl27.Text=null;
        lbl28.Text=null;
        lbl31.Text=null;
        lbl32.Text=null;
        lbl33.Text=null;
        lbl34.Text=null;
        lbl35.Text=null;
        lbl36.Text=null;
        lbl37.Text=null;
        lbl38.Text=null;
        lbl41.Text=null;
        lbl42.Text=null;
        lbl43.Text=null;
        lbl44.Text=null;
        lbl45.Text=null;
        lbl46.Text=null;
        lbl47.Text=null;
        lbl48.Text=null;
        lbl51.Text=null;
        lbl52.Text=null;
        lbl53.Text=null;
        lbl54.Text=null;
        lbl55.Text=null;
        lbl56.Text=null;
        lbl57.Text=null;
        lbl58.Text=null;
        lbl61.Text=null;
        lbl62.Text=null;
        lbl63.Text=null;
        lbl64.Text=null;
        lbl65.Text=null;
        lbl66.Text=null;
        lbl67.Text=null;
        lbl68.Text=null;
        lbl71.Text=null;
        lbl72.Text=null;
        lbl73.Text=null;
        lbl74.Text=null;
        lbl75.Text=null;
        lbl76.Text=null;
        lbl77.Text=null;
        lbl78.Text=null;
        lbl81.Text=null;
        lbl82.Text=null;
        lbl83.Text=null;
        lbl84.Text=null;
        lbl85.Text=null;
        lbl86.Text=null;
        lbl87.Text=null;
        lbl88.Text=null;
        lbl11.BackColor = Color.Transparent;
        lbl12.BackColor = Color.Transparent;
        lbl13.BackColor = Color.Transparent;
        lbl14.BackColor = Color.Transparent;
        lbl15.BackColor = Color.Transparent;
        lbl16.BackColor = Color.Transparent;
        lbl17.BackColor = Color.Transparent;
        lbl18.BackColor = Color.Transparent;
        lbl21.BackColor = Color.Transparent;
        lbl22.BackColor = Color.Transparent;
        lbl23.BackColor = Color.Transparent;
        lbl24.BackColor = Color.Transparent;
        lbl25.BackColor = Color.Transparent;
        lbl26.BackColor = Color.Transparent;
        lbl27.BackColor = Color.Transparent;
        lbl28.BackColor = Color.Transparent;
        lbl31.BackColor = Color.Transparent;
        lbl32.BackColor = Color.Transparent;
        lbl33.BackColor = Color.Transparent;
        lbl34.BackColor = Color.Transparent;
        lbl35.BackColor = Color.Transparent;
        lbl36.BackColor = Color.Transparent;
        lbl37.BackColor = Color.Transparent;
        lbl38.BackColor = Color.Transparent;
        lbl41.BackColor = Color.Transparent;
        lbl42.BackColor = Color.Transparent;
        lbl43.BackColor = Color.Transparent;
        lbl44.BackColor = Color.Transparent;
        lbl45.BackColor = Color.Transparent;
        lbl46.BackColor = Color.Transparent;
        lbl47.BackColor = Color.Transparent;
        lbl48.BackColor = Color.Transparent;
        lbl51.BackColor = Color.Transparent;
        lbl52.BackColor = Color.Transparent;
        lbl53.BackColor = Color.Transparent;
        lbl54.BackColor = Color.Transparent;
        lbl55.BackColor = Color.Transparent;
        lbl56.BackColor = Color.Transparent;
        lbl57.BackColor = Color.Transparent;
        lbl58.BackColor = Color.Transparent;
        lbl61.BackColor = Color.Transparent;
        lbl62.BackColor = Color.Transparent;
        lbl63.BackColor = Color.Transparent;
        lbl64.BackColor = Color.Transparent;
        lbl65.BackColor = Color.Transparent;
        lbl66.BackColor = Color.Transparent;
        lbl67.BackColor = Color.Transparent;
        lbl68.BackColor = Color.Transparent;
        lbl71.BackColor = Color.Transparent;
        lbl72.BackColor = Color.Transparent;
        lbl73.BackColor = Color.Transparent;
        lbl74.BackColor = Color.Transparent;
        lbl75.BackColor = Color.Transparent;
        lbl76.BackColor = Color.Transparent;
        lbl77.BackColor = Color.Transparent;
        lbl78.BackColor = Color.Transparent;
        lbl81.BackColor = Color.Transparent;
        lbl82.BackColor = Color.Transparent;
        lbl83.BackColor = Color.Transparent;
        lbl84.BackColor = Color.Transparent;
        lbl85.BackColor = Color.Transparent;
        lbl86.BackColor = Color.Transparent;
        lbl87.BackColor = Color.Transparent;
        lbl88.BackColor = Color.Transparent;
        tableLayoutPanel1.Update();
    }

    public void SetTimeOut(int timeoutInSec)
    {
        this.timeLeft = timeoutInSec;
        stopGame.Start();
    }

    public async Task WaitSomeTime(int timeInTicks)
    {
        await Task.Delay(timeInTicks);
    }

    public async Task SetCellIcon(Cell cell, Icon icon)
    {
        if (tableLayoutPanel1.GetControlFromPosition(cell.Column, cell.Row) is Label lbl)
        {
            lbl.Visible = true;
            lbl.Text = icon.Image;
            lbl.BackColor = icon.Color;
            await WaitSomeTime(10);

            lbl.Update();
        }
    }

    public async Task SetCellState(Cell cell, Icon icon, bool activ)
    {
        if (tableLayoutPanel1.GetControlFromPosition(cell.Column, cell.Row) is Label lbl)
        {
            lbl.Visible = true;
            lbl.BackColor = activ
                ? Color.OrangeRed
                : icon.Color;

            await WaitSomeTime(10);

            lbl.Update();
        }
    }

    public async Task RemoveCellIcon(Cell cell)
    {
        if (tableLayoutPanel1.GetControlFromPosition(cell.Column, cell.Row) is Label lbl)
        {
            lbl.Visible = false;
            lbl.Text = null;
            lbl.BackColor = Color.Transparent;
            await WaitSomeTime(20);

            lbl.Update();
        }
    }

    public async Task ReplaceCellIconImage(Cell cell, Icon icon, string altImage)
    {
        if (tableLayoutPanel1.GetControlFromPosition(cell.Column, cell.Row) is Label lbl)
        {
            lbl.Visible = true;
            lbl.BackColor = icon.Color;
            lbl.Text = altImage;
            await WaitSomeTime(20);

            lbl.Update();
        }
    }


    private void InitializeEvents()
    {
        lbl11.Click += async (s, a) => await lbl_Click(s);
        lbl12.Click += async (s, a) => await lbl_Click(s);
        lbl13.Click += async (s, a) => await lbl_Click(s);
        lbl14.Click += async (s, a) => await lbl_Click(s);
        lbl15.Click += async (s, a) => await lbl_Click(s);
        lbl16.Click += async (s, a) => await lbl_Click(s);
        lbl17.Click += async (s, a) => await lbl_Click(s);
        lbl18.Click += async (s, a) => await lbl_Click(s);
        lbl21.Click += async (s, a) => await lbl_Click(s);
        lbl22.Click += async (s, a) => await lbl_Click(s);
        lbl23.Click += async (s, a) => await lbl_Click(s);
        lbl24.Click += async (s, a) => await lbl_Click(s);
        lbl25.Click += async (s, a) => await lbl_Click(s);
        lbl26.Click += async (s, a) => await lbl_Click(s);
        lbl27.Click += async (s, a) => await lbl_Click(s);
        lbl28.Click += async (s, a) => await lbl_Click(s);
        lbl31.Click += async (s, a) => await lbl_Click(s);
        lbl32.Click += async (s, a) => await lbl_Click(s);
        lbl33.Click += async (s, a) => await lbl_Click(s);
        lbl34.Click += async (s, a) => await lbl_Click(s);
        lbl35.Click += async (s, a) => await lbl_Click(s);
        lbl36.Click += async (s, a) => await lbl_Click(s);
        lbl37.Click += async (s, a) => await lbl_Click(s);
        lbl38.Click += async (s, a) => await lbl_Click(s);
        lbl41.Click += async (s, a) => await lbl_Click(s);
        lbl42.Click += async (s, a) => await lbl_Click(s);
        lbl43.Click += async (s, a) => await lbl_Click(s);
        lbl44.Click += async (s, a) => await lbl_Click(s);
        lbl45.Click += async (s, a) => await lbl_Click(s);
        lbl46.Click += async (s, a) => await lbl_Click(s);
        lbl47.Click += async (s, a) => await lbl_Click(s);
        lbl48.Click += async (s, a) => await lbl_Click(s);
        lbl51.Click += async (s, a) => await lbl_Click(s);
        lbl52.Click += async (s, a) => await lbl_Click(s);
        lbl53.Click += async (s, a) => await lbl_Click(s);
        lbl54.Click += async (s, a) => await lbl_Click(s);
        lbl55.Click += async (s, a) => await lbl_Click(s);
        lbl56.Click += async (s, a) => await lbl_Click(s);
        lbl57.Click += async (s, a) => await lbl_Click(s);
        lbl58.Click += async (s, a) => await lbl_Click(s);
        lbl61.Click += async (s, a) => await lbl_Click(s);
        lbl62.Click += async (s, a) => await lbl_Click(s);
        lbl63.Click += async (s, a) => await lbl_Click(s);
        lbl64.Click += async (s, a) => await lbl_Click(s);
        lbl65.Click += async (s, a) => await lbl_Click(s);
        lbl66.Click += async (s, a) => await lbl_Click(s);
        lbl67.Click += async (s, a) => await lbl_Click(s);
        lbl68.Click += async (s, a) => await lbl_Click(s);
        lbl71.Click += async (s, a) => await lbl_Click(s);
        lbl72.Click += async (s, a) => await lbl_Click(s);
        lbl73.Click += async (s, a) => await lbl_Click(s);
        lbl74.Click += async (s, a) => await lbl_Click(s);
        lbl75.Click += async (s, a) => await lbl_Click(s);
        lbl76.Click += async (s, a) => await lbl_Click(s);
        lbl77.Click += async (s, a) => await lbl_Click(s);
        lbl78.Click += async (s, a) => await lbl_Click(s);
        lbl81.Click += async (s, a) => await lbl_Click(s);
        lbl82.Click += async (s, a) => await lbl_Click(s);
        lbl83.Click += async (s, a) => await lbl_Click(s);
        lbl84.Click += async (s, a) => await lbl_Click(s);
        lbl85.Click += async (s, a) => await lbl_Click(s);
        lbl86.Click += async (s, a) => await lbl_Click(s);
        lbl87.Click += async (s, a) => await lbl_Click(s);
        lbl88.Click += async (s, a) => await lbl_Click(s);

        this.btnStart.Click += async (s, a) => await presenter.StartGame();
        this.btnGameOver.Click += (s, e) => presenter.GameOver();
    }

    private async Task lbl_Click(object? sender)
    {
        if (sender == null)
            return;

        var position = this.tableLayoutPanel1.GetCellPosition((Control)sender);
        await presenter.ClickOnIcon(new Cell(position.Row, position.Column));
    }

    private void stopGame_Tick(object sender, EventArgs e)
    {
        timeLeft--;
        if (timeLeft <= 0)
        {
            stopGame.Stop();
        }

        this.lblTimeOut.Text = timeLeft.ToString();
        this.lblTimeOut.Update();

    }
}