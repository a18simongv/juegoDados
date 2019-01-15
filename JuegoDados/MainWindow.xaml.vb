Class MainWindow

    Dim marcJug1, marcJug2 As Short
    Dim jug1 As Boolean

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs)
        canvasBot.Visibility = Visibility.Hidden
    End Sub

    Private Sub ComenzarClick(sender As Object, e As RoutedEventArgs)

        resetCanvas()

        If tBxJg1.Text = "" Or tBxJg2.Text = "" Then
            MsgBox("Falta algún nombre", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Not IsNumeric(tBxTiradas.Text) Then
            MsgBox("El campo tiradas tiene que ser numérico", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        lblJg1.Content = tBxJg1.Text
        lblJg2.Content = tBxJg2.Text

        btnTirar.Content = "Tira " & tBxJg1.Text

        canvasBot.Visibility = Visibility.Visible

        canvasTop.IsEnabled = False
    End Sub

    Private Sub resetCanvas()
        lblMarc1.Content = "0"
        lblMarc2.Content = "0"
        lsvJg1.Items.Clear()
        lsvJg2.Items.Clear()

        marcJug1 = 0
        marcJug2 = 0

        jug1 = True

        canvasBot.IsEnabled = True

        Randomize(Date.Now.Millisecond)
    End Sub

    Private Sub btnTirar_Click(sender As Object, e As RoutedEventArgs)
        Dim valorDado As Byte
        valorDado = Int(Rnd() * 6) + 1

        Dim img As New BitmapImage
        img.BeginInit()
        img.UriSource = New Uri("L:\DesenvolvementoInterfaces\Proxectos\JuegoDados\dado\Dado" & valorDado & ".JPG")
        img.EndInit()

        imgDado.Stretch = Stretch.Fill
        imgDado.Source = img

        If jug1 Then
            lsvJg1.Items.Add(valorDado)
            marcJug1 += valorDado
            lblMarc1.Content = marcJug1.ToString

            btnTirar.Content = "Tira " & tBxJg2.Text
        Else
            lsvJg2.Items.Add(valorDado)
            marcJug2 += valorDado
            lblMarc2.Content = marcJug2.ToString

            btnTirar.Content = "Tira " & tBxJg1.Text

            If lsvJg2.Items.Count = tBxTiradas.Text Then
                canvasTop.IsEnabled = True
                canvasBot.IsEnabled = False

                showWinner()
            End If
        End If

        jug1 = Not jug1
    End Sub

    Private Sub showWinner()
        Dim message As String

        If marcJug1 > marcJug2 Then
            message = "Gano " & tBxJg1.Text
        ElseIf marcJug2 > marcJug1 Then
            message = "Gano " & tBxJg2.Text
        Else
            message = "Empataron"
        End If

        MsgBox(message, MsgBoxStyle.Information)
    End Sub

End Class
