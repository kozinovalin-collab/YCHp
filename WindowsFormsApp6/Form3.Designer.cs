using System.Windows.Forms;
using System;

partial class Form3
{
    private System.ComponentModel.IContainer components = null;

    private TextBox txtPhone;
    private TextBox txtPassword;
    private TextBox txtConfirmPassword;
    private Button btnRegister;
    private Label lblPhone;
    private Label lblPassword;
    private Label lblConfirmPassword;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.txtPhone = new TextBox();
        this.txtPassword = new TextBox();
        this.txtConfirmPassword = new TextBox();
        this.btnRegister = new Button();
        this.lblPhone = new Label();
        this.lblPassword = new Label();
        this.lblConfirmPassword = new Label();

        // Form
        this.SuspendLayout();
        this.Text = "Регистрация";
        this.ClientSize = new System.Drawing.Size(300, 200);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Phone Label
        this.lblPhone.Text = "Телефон:";
        this.lblPhone.Location = new System.Drawing.Point(20, 20);
        this.lblPhone.Size = new System.Drawing.Size(100, 20);

        // Phone TextBox
        this.txtPhone.Location = new System.Drawing.Point(120, 20);
        this.txtPhone.Size = new System.Drawing.Size(150, 20);
        this.txtPhone.TextChanged += new EventHandler(this.txtPhone_TextChanged);

        // Password Label
        this.lblPassword.Text = "Пароль:";
        this.lblPassword.Location = new System.Drawing.Point(20, 60);
        this.lblPassword.Size = new System.Drawing.Size(100, 20);

        // Password TextBox
        this.txtPassword.Location = new System.Drawing.Point(120, 60);
        this.txtPassword.Size = new System.Drawing.Size(150, 20);
        this.txtPassword.PasswordChar = '*';

        // Confirm Password Label
        this.lblConfirmPassword.Text = "Подтверждение:";
        this.lblConfirmPassword.Location = new System.Drawing.Point(20, 100);
        this.lblConfirmPassword.Size = new System.Drawing.Size(100, 20);

        // Confirm Password TextBox
        this.txtConfirmPassword.Location = new System.Drawing.Point(120, 100);
        this.txtConfirmPassword.Size = new System.Drawing.Size(150, 20);
        this.txtConfirmPassword.PasswordChar = '*';

        // Register Button
        this.btnRegister.Text = "Зарегистрироваться";
        this.btnRegister.Location = new System.Drawing.Point(80, 140);
        this.btnRegister.Size = new System.Drawing.Size(140, 30);
        this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

        // Add controls to form
        this.Controls.AddRange(new Control[] {
            this.lblPhone, this.txtPhone,
            this.lblPassword, this.txtPassword,
            this.lblConfirmPassword, this.txtConfirmPassword,
            this.btnRegister
        });

        this.ResumeLayout(false);
    }
}