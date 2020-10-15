Imports System.Drawing.Drawing2D
Imports System.Drawing.Bitmap
Public Class Form1
    'VB.NET 2019
#Region "COMMENTI ITALIANO"
    ' nome programma Mandala (MandalaVB03)
    ' nome autore    Dado57
    ' Crea forme, con ripetizione circolare e specchiatura tipo MANDALA
    ' Permette una striratura e posizionamento della forma
    ' Con colorazione random o manuale
    '
    ' FORMA
    ' La figura puo avere da 3 a 30 segmenti e viene disegnata entro i limiti
    ' del Raggio Minimo e Massimo, Angolo(in Gradi) Minimo e Massimo.
    ' Crea nuova forma: utilizza i valori impostati
    ' Limiti Random: crea i limini in random e poi crea la forma
    '
    ' GEOMETRIA
    ' Specchia: Le figure possono essere specchiate
    ' Numero Punte: La figura viene ripetuta ruotata da 1 a 360 volte
    ' Tensione:(0..1) La figura puo avere angoli acuti(0) o arrotondati(1)
    ' Posizione Angolare:(0..360) Ruota tutta la figura
    ' Moltip.Angolare:(0..100) Cambia posizione ai punti variando l'angolo
    ' Posizione Radiale:(-600..300) Cambia posizione ai punti variando il raggio
    ' Moltip.Radiale:(0.05..6) Fattore di scala sul raggio		
    '
    ' GRADIENTE
    ' Per il Colore interno si puo segliere il Gradiente tra 2 colori
    ' Click su 1 o 2 per segliere i colori
    ' Si puo modificare in Larghezza,Posizione e Angolo
    '
    ' COLORE INTERNO
    ' Si puo disabilitare o segliere tra: Colore1, Colore2 Gradiente1-2
    ' oppure Random (seglie un colore per tutte le punte) e
    ' Total Random (seglie un colore per ogni punta)
    ' Si puo variare la trasparenza (1 pieno- 0.1 molto trasparente)
    ' 
    ' RICREA COLORI RANDOM
    ' Rigenera la figura con nuovi colori random
    ' 
    ' COLORE BORDO
    ' Click su 3 per segliere un colore
    ' Si puo disabilitare o segliere tra: Colore1, Colore2 Colore3
    ' oppure Random (seglie un colore per i bordi)
    ' Spessore:(1..20) Seglie lo spessore del bordo
    ' Si puo variare la trasparenza (1 pieno- 0.1 molto trasparente)
    '
    ' COLORE SFONDO
    ' Click su S1 e S2 per segliere i colori
    ' Si puo disabilitare o segliere tra: ColoreS1, ColoreS2, Radiale S1-S2
    ' Radiale Espanso S1-S2
    ' Scambia: Scambia i colori S1 e S2
    ' Traspar: se on rende lo sfondo trasparente (per il salvataggio in PNG)
    ' Pixel: crea quadrati della dimensione indicata
    ' Stella: crea raggi del numero indicato
    ' Scelta colori per Pixel e Stella tra S1 e S2
    ' oppure TotRand: seglie tra tutti i colori
    ' Si puo variare la trasparenza (1 pieno- 0.1 molto trasparente)
    '
    ' TIENI
    ' Salva in memoria il disegno e permette di aggiungere altre forme
    '
    ' CANCELLA TUTTO
    ' Cancella sia il disegno che la memoria
    '
    ' SALVA
    ' Salva un file .PNG (con sfondo trasparente se indicato)
    '
    ' CARICA
    ' Carica un file .PNG
    '
    ' Per disegnarci sopra, prima premere TIENI, altrimenti sovrascrive

#End Region

#Region "COMMENTS ENGLISH"
    ' program name Mandala (MandalaVB03)
    ' author name  Dado57
    ' Create shapes, with circular repetition and MANDALA-like mirroring
    ' Allows for stretching and positioning of the last
    ' With random or manual coloring
    '
    ' SHAPE
    ' The figure can have from 3 to 30 segments and is drawn within the limits
    ' of the Minimum and Maximum Radius, Minimum and Maximum Angle (in Degrees).
    ' Create new shape: use the set values
    ' Random Limits: create the limini in random and then create the shape
    '
    ' GEOMETRY
    ' Mirror: Figures can be mirrored
    ' Number of Points: The figure is repeated rotated from 1 to 360 times
    ' Tension: (0..1) The figure can have sharp (0) or rounded (1) corners
    ' Angular Position: (0..360) Rotate the whole figure
    ' Multip.Angular: (0..100) Changes the position of the points by varying the angle
    ' Radial Position: (- 600..300) Change position of points by varying the radius
    ' Multip.Radial: (0.05..6) Scale factor on the radius
    '
    ' GRADIENT
    ' For the Inner Color you can choose the Gradient between 2 colors
    ' Click on 1 or 2 to choose the colors
    ' You can change it in Width, Position and Angle
    '
    ' INTERNAL COLOR
    ' You can disable or choose between: Color1, Color2 Gradient1-2
    ' or Random (choose a color for all points) e
    ' Total Random (choose a color for each point)
    ' You can vary the transparency (1 full - 0.1 very transparent)
    '
    ' BACKGROUND COLOR
    ' Click on S1 and S2 to choose the colors
    ' You can disable or choose between: Color S1, Color S2, Radial S1-S2
    ' Expanded Radial S1-S2
    ' Swap: Swap the S1 and S2 colors
    ' Traspar: if on it makes the background transparent (for saving in PNG)
    ' Pixel: creates squares of the indicated size
    ' Star: creates rays of the indicated number
    ' Color choice for Pixel and Stella between S1 and S2
    ' or TotRand: choose between all colors
    ' You can vary the transparency (1 full - 0.1 very transparent)
    '
    ' KEEP
    ' Saves the drawing in memory and allows you to add other shapes
    '
    ' ERASE EVERYTHING
    ' Erases both the drawing and the memory
    '
    ' SAVE
    ' Save a .PNG file (with transparent background if indicated)
    '
    ' CHARGE
    ' Upload a .PNG file
    '
    ' To draw on it, first press HOLD, otherwise it overwrites

#End Region

#Region "Variabili Globali"
    Private ReadOnly m_Rnd As New Random
    Private gDisegna As Boolean = False
    Private gWP As Integer          ' Width Penna larghezza
    Private gNP As Integer          ' Numero Punte
    Private gNL As Integer = 5      ' Numero Lati
    Private grMin As Single         ' dimensione Raggio Minimo
    Private grMax As Single         ' dimensione Raggio Massimo
    Private gaMin As Single         ' Angolo Minimo (Radianti)
    Private gaMax As Single         ' Angolo Massimo (Radianti)
    Private gaRot As Single         ' Angolo Rotazione (Radianti)
    Private gMolR As Single = 1.0   ' Fattore moltiplicazione Raggio
    Private gMolA As Single = 1.0   ' Fattore moltiplicazione Angolo
    Private gPosR As Single = 0     ' Posizione Radiale
    Private gcX As Single           ' Coordinata X del centro
    Private gcY As Single           ' Coordinata Y del centro
    Private gaTens As Single        ' Valore di Tensione tar 0=Angoli a punta e 1=Angoli massima curvatura
    Private gFill As Boolean = True ' Flag figura riempita
    Private gOutL As Boolean = True ' Flag contorno figura
    Private gSpec As Boolean = True ' Flag se figura viene specchiata
    Private gCSF1 As Color          ' Colore sfondo
    Private gCSF2 As Color          ' Colore sfondo  
    Private gC1 As Color            ' Colore 1
    Private gC2 As Color            ' Colore 2
    Private gC3 As Color            ' Colore 3
    Private gCFr As Color           ' Colore Fill    Random (Riempimento)
    Private gCOr As Color           ' Colore Outline Random (Bordo)
    Private gTrSf As Single = 1     ' Valore Trasparenza Sfondo 
    Private gFillMode As Integer = 1  ' Scelta del modo di Riempimento
    Private gOutCMode As Integer = 2  ' Scelta del modo del Bordo
    Private ReadOnly lPol As New List(Of PolarF) ' Lista dei punti di partenza (in coordinate Polari)
    Private bmpMemoria = New Bitmap(640, 640)
    ''' <summary>
    ''' Struttura Coordinate Polari
    ''' </summary>
    Private Structure PolarF
        Public L As Single ' Raggio in pixel
        Public A As Single ' Angolo in radianti
    End Structure
    ''' <summary>
    ''' Struttura 3 Single
    ''' </summary>
    Private Structure Single3
        Public Min As Single
        Public Max As Single
        Public Med As Single
    End Structure
    ''' <summary>
    ''' Struttura Linea 
    ''' </summary>
    Private Structure LineF
        Public P1 As PointF
        Public P2 As PointF
    End Structure

#End Region

#Region "Form e Button"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gcX = PB.Height / 2
        gcY = PB.Width / 2
        gWP = 1
        gNP = 5 'Numero Punte 
        grMin = 10 'lunghezza minima
        grMax = 300 'lunghezza massima
        gaMin = -36 / 180 * Math.PI
        gaMax = 36 / 180 * Math.PI
        gaRot = 0
        gaTens = 0.5
        gC1 = L_C1.BackColor
        gC2 = L_C2.BackColor
        gCSF1 = L_ColSfon1.BackColor
        gCSF2 = L_ColSfon2.BackColor
        Dim tras As Integer = HSB_Trasp.Value / 10 * 255
        gC1 = Color.FromArgb(tras, gC1.R, gC1.G, gC1.B)
        gC2 = Color.FromArgb(tras, gC2.R, gC2.G, gC2.B)
        MostraBrus(gC1, gC2)
        CreaPolar(gNP)
        PB.BackgroundImage = New Bitmap(PB.Width, PB.Height)
    End Sub
    Private Sub B_CreaPolar_Click(sender As Object, e As EventArgs) Handles B_CreaPolar.Click
        CreaPolar(gNL)
    End Sub
    Private Sub B_Crea_Click(sender As Object, e As EventArgs) Handles B_Crea.Click
        Mandala(lPol)
    End Sub
    Private Sub B_Cancella_Click(sender As Object, e As EventArgs) Handles B_Cancella.Click
        Dim bmpBianco = New Bitmap(PB.Width, PB.Height)
        PB_ico.Image = New Bitmap(bmpBianco, PB_ico.Width, PB_ico.Height)
        PB_ico.BackgroundImage = New Bitmap(bmpBianco, PB_ico.Width, PB_ico.Height)
        PB.Image = New Bitmap(bmpBianco)
        PB.BackgroundImage = New Bitmap(bmpBianco)
        bmpMemoria = bmpBianco.Clone
        gDisegna = False
    End Sub
    Private Sub B_Tieni_Click(sender As Object, e As EventArgs) Handles B_Tieni.Click
        PB_ico.BackgroundImage = New Bitmap(PB.BackgroundImage, PB_ico.Width, PB_ico.Height)
        PB_ico.Image = New Bitmap(PB.Image, PB_ico.Width, PB_ico.Height)
        bmpMemoria = PB.Image.Clone
    End Sub

#End Region

#Region "Sub DISEGNO PRINCIPALE - MAIN DRAWING "

    ''' <summary>
    ''' Crea una lista di PolarF (punti in coodinate polari)
    ''' e chiama Mandala
    ''' </summary>
    ''' <param name="NumeroPunti"></param>
    Private Sub CreaPolar(NumeroPunti As Integer)
        lPol.Clear()
        gMolA = 1 : NUD_MolA.Value = 1
        gMolR = 1 : NUD_MolR.Value = 1
        gPosR = 0 : NUD_PosR.Value = 0

        For i = 1 To NumeroPunti
            lPol.Add(CreaRandPolar(grMin, grMax, gaMin, gaMax))
        Next
        Mandala(lPol)
    End Sub
    ''' <summary>
    ''' Ritorna .Min .Max .Med di L
    ''' </summary>
    ''' <param name="Po">List of PolarF</param>
    ''' <returns>list of double</returns>
    Private Function PMiMaMe(Po As List(Of PolarF)) As Single3
        Dim R As Single3
        R.Min = 1000
        R.Max = 0
        For Each p As PolarF In Po
            If p.L > R.Max Then R.Max = p.L
            If p.L < R.Min Then R.Min = p.L
        Next
        R.Med = R.Min + (R.Max - R.Min) / 2
        Return R
    End Function
    Private Sub ModificaPolar()
        Dim lMod As New List(Of PolarF)
        Dim R = PMiMaMe(lPol)
        For Each i As PolarF In lPol
            Dim m As PolarF
            m.L = R.Med + (i.L - R.Med) * gMolR + gPosR
            m.A = i.A * gMolA
            lMod.Add(m)
        Next
        Mandala(lMod)
    End Sub

    ''' <summary>
    ''' Chiamata per Mandala se gDisegna=True
    ''' </summary>
    Private Sub CallMandala()
        If gDisegna Then
            ModificaPolar()
        End If
    End Sub

    ''' <summary>
    ''' "DISEGNO PRINCIPALE - MAIN DRAWING "
    ''' </summary>
    Private Sub Mandala(lP)
        ' Dim Img = New Bitmap(PB.Width, PB.Height)
        Dim Img As Image = bmpMemoria.clone
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        Dim pC As New PointF(gcX, gcY)

        gCFr = RandColor()

        Dim Tras As Integer = HSB_Trasp.Value / 10 * 255
        gC1 = Color.FromArgb(Tras, gC1.R, gC1.G, gC1.B)
        gC2 = Color.FromArgb(Tras, gC2.R, gC2.G, gC2.B)
        Dim ColorBlend As New ColorBlend With {
            .Colors = New Color() {gC1, gC2, gC1},
            .Positions = New Single() {0.0F, 0.5F, 1.0F}}

        gCOr = CreaColorOut(gOutCMode)
        Dim pBordo As New Pen(gCOr, gWP)

        Dim iLmin As Integer
        Dim iLmax As Integer
        Dim Lmin As Single = gcY
        Dim Lmax As Single = 0
        Dim a As Single
        For i = 0 To gNP - 1
            Dim lpcG As New List(Of PointF)
            Dim lpcS As New List(Of PointF)
            a = 2 * Math.PI / gNP * i - Math.PI / 2
#Region "(Per controllo)"
            'Dim z1 As PointF
            'z1.X = gcX + gcX * Math.Cos(a)
            'z1.Y = gcY + gcY * Math.Sin(a)
            'g.DrawLine(Pens.Blue, pC, z1)
#End Region
            For p = 0 To lPol.Count - 1
                Dim Pg As PointF = Ruota(pC, lP(p), a + gaRot)
                Dim Ps As PointF = Specchia(pC, lP(p), a + gaRot)
                'cerco i valori minimo e massimo dei punti rispetto a 0
                If lP(p).L < Lmin Then iLmin = p : Lmin = lP(p).L
                If lP(p).L > Lmax Then iLmax = p : Lmax = lP(p).L
                lpcG.Add(Pg)
                lpcS.Add(Ps)
            Next

            If gOutL Then
                g.DrawClosedCurve(pBordo, lpcG.ToArray(), gaTens, 1)
                If gSpec Then g.DrawClosedCurve(pBordo, lpcS.ToArray(), gaTens, 1)
            End If

            If gFillMode = 4 Then
                If gFill Then
                    Dim P1g As PolarF = PolarFromCart(pC, lpcG(iLmin))
                    Dim P2g As PolarF = PolarFromCart(pC, lpcG(iLmax))
                    Dim lineaG As LineF = CreaLineaGradiente(pC, P1g, P2g, 1)
                    Dim bRandG As New LinearGradientBrush(lineaG.P1, lineaG.P2, gC1, gC2) With {
                        .InterpolationColors = ColorBlend}
                    g.FillClosedCurve(bRandG, lpcG.ToArray(), 1, gaTens)
                    bRandG.Dispose()
                    Dim pC1 As PointF : pC1 = CartFromPolar(pC, P1g)
                    Dim pC2 As PointF : pC2 = CartFromPolar(pC, P2g)
#Region "(Per controllo)"
                    'g.DrawLine(Pens.Black, lpcG(iLmin), lpcG(iLmax))
                    'g.FillEllipse(Brushes.Blue, lineaG.P1.X - 2, lineaG.P1.Y - 2, 4, 4)
                    'g.FillEllipse(Brushes.Black, lineaG.P2.X - 2, lineaG.P2.Y - 2, 4, 4)
#End Region
                    If gSpec Then
                        Dim P1s As PolarF = PolarFromCart(pC, lpcS(iLmin))
                        Dim P2s As PolarF = PolarFromCart(pC, lpcS(iLmax))
                        Dim lineaS As LineF = CreaLineaGradiente(pC, P1s, P2s, -1)
                        Dim bRandS As New LinearGradientBrush(lineaS.P1, lineaS.P2, gC1, gC2) With {
                        .InterpolationColors = ColorBlend}
                        g.FillClosedCurve(bRandS, lpcS.ToArray(), 1, gaTens)
                        bRandS.Dispose()
#Region "(Per controllo)"
                        'g.DrawLine(Pens.Black, lineaS.P1, lineaS.P2)' per controllo
#End Region
                    End If

                End If
            Else
                If gFill Then
                    Dim P1g As PointF = lpcG(iLmin)
                    Dim P2g As PointF = lpcG(iLmax)
                    Dim bRandG = CreaBrusGra(gFillMode)
                    g.FillClosedCurve(bRandG, lpcG.ToArray(), 1, gaTens)
                    Dim P1s As PointF = lpcS(iLmin)
                    Dim P2s As PointF = lpcS(iLmax)
                    'Dim bRandS = CreaBrusGra(gFillMode)
                    If gSpec Then g.FillClosedCurve(bRandG, lpcS.ToArray(), 1, gaTens)
                    bRandG.Dispose()
                End If
            End If

        Next
        PB.Image = Img
        pBordo.Dispose()
        g.Dispose()
        gDisegna = True
    End Sub
#End Region

#Region "Parametri FORMA"
    Private Sub NUD_NuLati_ValueChanged(sender As Object, e As EventArgs) Handles NUD_NuLati.ValueChanged
        gNL = NUD_NuLati.Value ' Numero Punte
    End Sub
    Private Sub NUD_RaMin_ValueChanged(sender As Object, e As EventArgs) Handles NUD_RaMin.ValueChanged
        grMin = NUD_RaMin.Value
    End Sub
    Private Sub NUD_RaMax_ValueChanged(sender As Object, e As EventArgs) Handles NUD_RaMax.ValueChanged
        grMax = NUD_RaMax.Value
    End Sub
    Private Sub NUD_AnMin_ValueChanged(sender As Object, e As EventArgs) Handles NUD_AnMin.ValueChanged
        gaMin = NUD_AnMin.Value / 180 * Math.PI
    End Sub
    Private Sub NUD_AnMax_ValueChanged(sender As Object, e As EventArgs) Handles NUD_AnMax.ValueChanged
        gaMax = NUD_AnMax.Value / 180 * Math.PI
    End Sub
    Private Sub B_LimitiRand_Click(sender As Object, e As EventArgs) Handles B_LimitiRand.Click
        grMin = Rnd() * 360 - 40   ' dimensione Raggio Minimo
        grMax = Rnd() * 320 + 0    ' dimensione Raggio Massimo
        gaMin = Rnd() * 240 + -120    ' Angolo Minimo (Radianti)
        gaMax = Rnd() * 240 + -120    ' Angolo Massimo (Radianti)
        NUD_AnMax.Value = gaMax '/ 180 * Math.PI
        NUD_AnMin.Value = gaMin '/ 180 * Math.PI
        NUD_RaMax.Value = grMax
        NUD_RaMin.Value = grMin
        CreaPolar(gNL)
    End Sub
    Private Sub NUD_MolR_ValueChanged(sender As Object, e As EventArgs) Handles NUD_MolR.ValueChanged
        If NUD_MolR.Value = 0 Then NUD_MolR.Value = 0.05 ' da errore per NUD_MolR.Minimum =0,1
        If gMolR <> NUD_MolR.Value Then
            gMolR = NUD_MolR.Value
            ModificaPolar()
        End If
    End Sub
    Private Sub NUD_Posizione_ValueChanged(sender As Object, e As EventArgs) Handles NUD_PosR.ValueChanged
        If gPosR <> NUD_PosR.Value Then
            gPosR = NUD_PosR.Value
            ModificaPolar()
        End If
    End Sub
    Private Sub NUD_MolA_ValueChanged(sender As Object, e As EventArgs) Handles NUD_MolA.ValueChanged
        If gMolA <> NUD_MolA.Value Then
            gMolA = NUD_MolA.Value
            ModificaPolar()
        End If
    End Sub
#End Region

#Region "Parametri GEOMETRIA"
    Private Sub CB_Spec_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Spec.CheckedChanged
        gSpec = CB_Spec.Checked
        CallMandala()
    End Sub
    Private Sub NUD_NP_ValueChanged(sender As Object, e As EventArgs) Handles NUD_NP.ValueChanged
        gNP = NUD_NP.Value ' Numero Punte
        CallMandala()
    End Sub
    Private Sub NUD_AnRot_ValueChanged(sender As Object, e As EventArgs) Handles NUD_AnRot.ValueChanged
        gaRot = NUD_AnRot.Value / 180 * Math.PI
        CallMandala()
    End Sub
    Private Sub NUD_Tens_ValueChanged(sender As Object, e As EventArgs) Handles NUD_Tens.ValueChanged
        gaTens = NUD_Tens.Value / 10
        CallMandala()
    End Sub
#End Region

#Region "Colori e GRADIENTE"
    Private Sub L_C2_Click(sender As Object, e As EventArgs) Handles L_C2.Click
        Dim MyDialog As New ColorDialog With {.Color = L_C2.BackColor}
        If (MyDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            gC2 = MyDialog.Color
            L_C2.BackColor = gC2
            L_C2.ForeColor = ContrastaColore(gC2)
        End If
        MostraBrus(gC1, gC2)
    End Sub
    Private Sub L_C1_Click(sender As Object, e As EventArgs) Handles L_C1.Click
        Dim MyDialog As New ColorDialog With {.Color = L_C1.BackColor}
        If (MyDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            gC1 = MyDialog.Color
            L_C1.BackColor = gC1
            L_C1.ForeColor = ContrastaColore(gC1)
        End If
        MostraBrus(gC1, gC2)
    End Sub

    Private Sub L_C3_Click(sender As Object, e As EventArgs) Handles L_C3.Click
        Dim MyDialog As New ColorDialog With {.Color = L_C3.BackColor}
        If (MyDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            gC3 = MyDialog.Color
            L_C3.BackColor = gC3
            L_C3.ForeColor = ContrastaColore(gC3)
        End If
        CallMandala()
    End Sub

    Private Sub HSB_BrusAng_Scroll(sender As Object, e As ScrollEventArgs) Handles HSB_BrusAng.Scroll
        MostraBrus(gC1, gC2)
    End Sub
    Private Sub HSB_BrusMol_Scroll(sender As Object, e As ScrollEventArgs) Handles HSB_BrusMol.Scroll
        MostraBrus(gC1, gC2)
    End Sub
    Private Sub HSB_BrusPos_Scroll(sender As Object, e As ScrollEventArgs) Handles HSB_BrusPos.Scroll
        MostraBrus(gC1, gC2)
    End Sub
    Private Function CreaBrusGra(Mode As Integer) As SolidBrush
        Dim r As SolidBrush
        Dim Tras As Single = HSB_Trasp.Value / 10 * 255
        Select Case Mode
            Case 1 'colore 1
                r = New SolidBrush(Color.FromArgb(Tras, gC1.R, gC1.G, gC1.B))
            Case 2 'colore 2
                r = New SolidBrush(Color.FromArgb(Tras, gC2.R, gC2.G, gC2.B))
            Case 3 'colore random
                r = New SolidBrush(gCFr)
            Case 5 'colore random
                r = New SolidBrush(RandColor())
            Case Else
                r = New SolidBrush(gC1)
        End Select
        Return r
    End Function
    Private Function CreaColorOut(Mode As Integer) As Color
        Dim r As Color
        Dim TrBordo As Single = HSB_TrBordo.Value / 10 ' 0..1
        Select Case Mode
            Case 1 'colore 1
                r = Color.FromArgb(TrBordo * 255, gC1.R, gC1.G, gC1.B)
            Case 2 'colore 2
                r = Color.FromArgb(TrBordo * 255, gC2.R, gC2.G, gC2.B)
            Case 3 'colore 3
                r = Color.FromArgb(TrBordo * 255, gC3.R, gC3.G, gC3.B)
            Case 4 'colore Random
                r = RandColorCT(TrBordo)
            Case Else
                r = gC1
        End Select
        Return r
    End Function
    Private Sub MostraBrus(C1 As Color, C2 As Color)
        L_BrusMol.Text = HSB_BrusMol.Value * 10
        L_BrusAng.Text = HSB_BrusAng.Value * 360 / 20
        L_BrusPos.Text = HSB_BrusPos.Value * 100 / 20
        Dim vm As Double = HSB_BrusMol.Value / 10
        Dim vp As Double = HSB_BrusPos.Value / 10
        Dim va As Double = HSB_BrusAng.Value / 10 * Math.PI
        Dim P1 As PointF
        Dim P2 As PointF
        Dim PC As PointF
        PC.X = 32
        PC.Y = 32
        Dim L As Double = 32 * vm
        P1.X = PC.X + (L + L * vp) * Math.Cos(va)
        P1.Y = PC.Y + (L + L * vp) * Math.Sin(va)
        P2.X = PC.X + (L - L * vp) * Math.Cos(va + Math.PI)
        P2.Y = PC.Y + (L - L * vp) * Math.Sin(va + Math.PI)

        Dim bGrad As New LinearGradientBrush(P1, P2, C1, C2)
        Dim Img = New Bitmap(64, 64)
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        g.FillRectangle(bGrad, 0, 0, 63, 63)
#Region "(per controllo)"
        'g.DrawLine(Pens.Black, P1, P2)
#End Region
        PB_Grad.Image = Img
        bGrad.Dispose()
        g.Dispose()
        If RB_Col1.Checked Or RB_CoB1.Checked Then CallMandala()
        If RB_Col2.Checked Or RB_CoB2.Checked Then CallMandala()
        If RB_Col4.Checked Then CallMandala()
    End Sub
#End Region

#Region "Colore INTERNO"
    Private Sub CB_Fill_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Fill.CheckedChanged
        gFill = CB_Fill.Checked
        If Not (gFill) Then CB_Out.Checked = True
        CallMandala()
    End Sub
    Private Sub RB_Col1_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Col1.CheckedChanged
        gFillMode = 1
        CallMandala()
    End Sub
    Private Sub RB_Col2_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Col2.CheckedChanged
        gFillMode = 2
        CallMandala()
    End Sub
    Private Sub RB_Col3_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Col3.CheckedChanged
        gFillMode = 3
        CallMandala()
    End Sub
    Private Sub RB_Col4_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Col4.CheckedChanged
        gFillMode = 4
        CallMandala()
    End Sub
    Private Sub RB_Col5_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Col5.CheckedChanged
        gFillMode = 5
        CallMandala()
    End Sub
    Private Sub HSB_Trasp_Scroll(sender As Object, e As ScrollEventArgs) Handles HSB_Trasp.Scroll
        L_Trasp.Text = HSB_Trasp.Value / 10
        CallMandala()
    End Sub
#End Region

#Region "Colore BORDI"
    Private Sub CB_Out_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Out.CheckedChanged
        gOutL = CB_Out.Checked
        If Not (gOutL) Then CB_Fill.Checked = True
        CallMandala()
    End Sub
    Private Sub NUD_WP_ValueChanged(sender As Object, e As EventArgs) Handles NUD_WP.ValueChanged
        gWP = NUD_WP.Value
        CallMandala()
    End Sub
    Private Sub RB_CoB1_CheckedChanged(sender As Object, e As EventArgs) Handles RB_CoB1.CheckedChanged
        gOutCMode = 1
        CallMandala()
    End Sub
    Private Sub RB_CoB2_CheckedChanged(sender As Object, e As EventArgs) Handles RB_CoB2.CheckedChanged
        gOutCMode = 2
        CallMandala()
    End Sub
    Private Sub RB_CoB3_CheckedChanged(sender As Object, e As EventArgs) Handles RB_CoB3.CheckedChanged
        gOutCMode = 3
        CallMandala()
    End Sub
    Private Sub RB_CoB4_CheckedChanged(sender As Object, e As EventArgs) Handles RB_CoB4.CheckedChanged
        gOutCMode = 4
        CallMandala()
    End Sub
    Private Sub HSB_TrBordo_Scroll(sender As Object, e As ScrollEventArgs) Handles HSB_TrBordo.Scroll
        L_TrBordo.Text = HSB_TrBordo.Value / 10
        CallMandala()
    End Sub

#End Region

#Region "Colore SFONDO"
    Private Sub HSB_TrSf_Scroll(sender As Object, e As ScrollEventArgs) Handles HSB_TrSf.Scroll
        gTrSf = HSB_TrSf.Value / 10
        L_TrSf.Text = gTrSf
        gCSF1 = AggiungiTrasp(gCSF1, gTrSf)
        gCSF2 = AggiungiTrasp(gCSF2, gTrSf)
    End Sub
    Private Function AggiungiTrasp(C As Color, T As Single) As Color
        Return Color.FromArgb(T * 255, C.R, C.G, C.B)
    End Function
    Private Sub L_ColSfondo_Click(sender As Object, e As EventArgs) Handles L_ColSfon1.Click
        Dim Dial As New ColorDialog With {.Color = L_ColSfon1.BackColor}
        If (Dial.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            gCSF1 = Dial.Color
            L_ColSfon1.BackColor = gCSF1
            L_ColSfon1.ForeColor = ContrastaColore(gCSF1)
            gCSF1 = AggiungiTrasp(gCSF1, gTrSf)
        End If
    End Sub
    Private Sub L_ColSfon2_Click(sender As Object, e As EventArgs) Handles L_ColSfon2.Click
        Dim Dial As New ColorDialog With {.Color = L_ColSfon2.BackColor}
        If (Dial.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            gCSF2 = Dial.Color
            L_ColSfon2.BackColor = gCSF2
            L_ColSfon2.ForeColor = ContrastaColore(gCSF2)
            gCSF1 = AggiungiTrasp(gCSF1, gTrSf)
        End If
    End Sub
    Private Sub B_CambiaS1S2_Click(sender As Object, e As EventArgs) Handles B_CambiaS1S2.Click
        Dim cProv As Color
        cProv = gCSF1 : gCSF1 = gCSF2 : gCSF2 = cProv
        L_ColSfon1.BackColor = gCSF1
        L_ColSfon1.ForeColor = ContrastaColore(gCSF1)
        L_ColSfon2.BackColor = gCSF2
        L_ColSfon2.ForeColor = ContrastaColore(gCSF2)
    End Sub
    Private Sub CB_SfondoTrasp_CheckedChanged(sender As Object, e As EventArgs) Handles CB_SfondoTrasp.CheckedChanged, CB_Sf_TotRand.CheckedChanged
        If CB_SfondoTrasp.Checked Then
            gCSF1 = ColorTranslator.FromOle(&HFFFFFF)
            L_ColSfon1.BackColor = gCSF1
            L_ColSfon1.ForeColor = ContrastaColore(gCSF1)
            Dim Img = New Bitmap(PB.Width, PB.Height)
            Dim g As Graphics = Graphics.FromImage(Img)
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            Dim bSfondo = New SolidBrush(gCSF1)
            g.FillRectangle(bSfondo, Me.ClientRectangle)
            PB.BackgroundImage = Img
        End If
    End Sub
    Private Sub B_Radial_Click(sender As Object, e As EventArgs) Handles B_Radial.Click
        Radial()
    End Sub
    Private Sub B_RadialeEsp_Click(sender As Object, e As EventArgs) Handles B_RadialeEsp.Click
        Radial(True)
    End Sub
    Private Sub Radial(Optional Esp As Boolean = False)
        Dim Img = New Bitmap(PB.Width, PB.Height)
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        Dim pth As New GraphicsPath()
        Dim rect As New Rectangle
        If Esp Then
            rect.X = -0.42 * PB.Width / 2
            rect.Y = -0.42 * PB.Height / 2
            rect.Width = PB.Width * 1.42
            rect.Height = PB.Height * 1.42
        Else
            rect = PB.ClientRectangle
        End If
        pth.AddEllipse(rect)
        Dim pgb As New PathGradientBrush(pth) With {
            .SurroundColors = New Color() {gCSF2},
            .CenterColor = gCSF1}
        g.FillRectangle(pgb, PB.ClientRectangle)
        PB.BackgroundImage = Img
    End Sub
    Private Sub B_Unif1_Click(sender As Object, e As EventArgs) Handles B_Unif1.Click
        Dim Img = New Bitmap(PB.Width, PB.Height)
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        Dim bSfondo = New SolidBrush(gCSF1)
        g.FillRectangle(bSfondo, Me.ClientRectangle)
        PB.BackgroundImage = Img
        CB_SfondoTrasp.Checked = False
    End Sub
    Private Sub B_Unif2_Click(sender As Object, e As EventArgs) Handles B_Unif2.Click
        Dim Img = New Bitmap(PB.Width, PB.Height)
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        Dim bSfondo = New SolidBrush(gCSF2)
        g.FillRectangle(bSfondo, Me.ClientRectangle)
        PB.BackgroundImage = Img
        CB_SfondoTrasp.Checked = False
    End Sub
    Private Sub B_Noise_Click(sender As Object, e As EventArgs) Handles B_Noise.Click
        Dim gNumPixel As Integer = NUD_Pixel.Value
        If gNumPixel = 1 Then
            Pixel_1()
        Else
            Pixel(gNumPixel)
        End If
    End Sub
    Private Sub Pixel_1()
        Dim x, y As Integer
        Dim bmp As Bitmap
        Dim Col As Color
        bmp = New System.Drawing.Bitmap(640, 640)
        For x = 0 To 639
            For y = 0 To 639
                If CB_Sf_TotRand.Checked Then
                    Col = RandColorCT(gTrSf)
                Else
                    Col = Rand2Color(gTrSf, gCSF1, gCSF2)
                End If
                bmp.SetPixel(x, y, Col)
            Next
        Next
        PB.BackgroundImage = bmp
    End Sub
    Private Sub Pixel(D As Single)
        Dim x, y As Single
        Dim Col As Color
        Dim Img As Image = bmpMemoria.clone
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        For x = 0 To 639 Step D
            For y = 0 To 639 Step D
                If CB_Sf_TotRand.Checked Then
                    Col = RandColorCT(gTrSf)
                Else
                    Col = Rand2Color(gTrSf, gCSF1, gCSF2)
                End If
                Dim b = New SolidBrush(Col)
                g.FillRectangle(b, x, y, D, D)
                b.Dispose()
            Next
        Next
        PB.BackgroundImage = Img
    End Sub
    Private Sub B_Raggi_Click(sender As Object, e As EventArgs) Handles B_Raggi.Click
        Dim an1, an2 As Single
        Dim Rag As Single = PB.Width * 1.42
        Dim NR As Single = NUD_Rag.Value
        Dim pas As Single = 360 / NR
        Dim P(2) As PointF
        P(0).X = gcX
        P(0).Y = gcY
        Dim Img As Image = bmpMemoria.clone
        Dim g As Graphics = Graphics.FromImage(Img)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
        Dim Col As Color
        For i = 0 To NR
            an1 = (pas * i) * Math.PI / 180 - Math.PI / 2
            an2 = (pas * (i + 1)) * Math.PI / 180 - Math.PI / 2
            P(1).X = Rag * Math.Cos(an1) + gcX
            P(1).Y = Rag * Math.Sin(an1) + gcY
            P(2).X = Rag * Math.Cos(an2) + gcX
            P(2).Y = Rag * Math.Sin(an2) + gcY
            If CB_Sf_TotRand.Checked Then
                Col = RandColorCT(gTrSf)
            Else
                Col = Rand2Color(gTrSf, gCSF1, gCSF2)
            End If
            Dim b As New SolidBrush(Col)
            g.FillPolygon(b, P)
            b.Dispose()
        Next
        PB.BackgroundImage = Img
    End Sub

#End Region

#Region "Function Color"
    ''' <summary>
    ''' Ritorna colore random tra 10 e 255 con trasparenza HSB_Trasp
    ''' </summary>
    ''' <returns>Colore</returns>
    Public Function RandColor() As Color
        Dim Tras As Integer = HSB_Trasp.Value / 10 * 255
        Return Color.FromArgb(Tras, m_Rnd.Next(10, 255), m_Rnd.Next(10, 255), m_Rnd.Next(10, 255))
    End Function

    ''' <summary>
    ''' Crea colore random con trasparenza
    ''' </summary>
    ''' <param name="T">Trasparenza tra 0 e 1</param>
    ''' <returns></returns>
    Public Function RandColorCT(T) As Color
        Return Color.FromArgb(T * 255, m_Rnd.Next(10, 255), m_Rnd.Next(10, 255), m_Rnd.Next(10, 255))
    End Function
    ''' <summary>
    ''' Ritorna colore random tra 2 colori
    ''' </summary>
    ''' <returns>Colore</returns>
    Public Function Rand2Color(T As Single, C1 As Color, C2 As Color) As Color
        Dim Rmin, Rmax, Gmin, Gmax, Bmin, Bmax As Integer
        Rmin = Math.Min(C1.R, C2.R)
        Rmax = Math.Max(C1.R, C2.R)
        Gmin = Math.Min(C1.G, C2.G)
        Gmax = Math.Max(C1.G, C2.G)
        Bmin = Math.Min(C1.B, C2.B)
        Bmax = Math.Max(C1.B, C2.B)
        Return Color.FromArgb(T * 255, m_Rnd.Next(Rmin, Rmax), m_Rnd.Next(Gmin, Gmax), m_Rnd.Next(Bmin, Bmax))
    End Function
    ''' <summary>
    ''' Ritorna un colore che contrasta rispetto al colore RGB 
    ''' per vedere il testo sulle label colorate
    ''' </summary>
    ''' <param name="RGB"></param>
    ''' <returns>Colore</returns>
    Private Function ContrastaColore(RGB As Color) As Color
        Dim r, g, b As Integer
        If RGB.R > 128 Then r = 0 Else r = 255
        If RGB.G > 128 Then g = 0 Else g = 255
        If RGB.B > 128 Then b = 0 Else b = 255
        Dim CC As Color
        CC = Color.FromArgb(r, g, b)
        Return CC
    End Function
#End Region

#Region "Function coordinate Polari e Cartesiane"
    ''' <summary>
    ''' Crea punto random tra RAGGIO dMin e dMax e tra ANGOLO aMin e aMax
    ''' </summary>
    ''' <param name="dMin">Misura</param>
    ''' <param name="dMax">Misura</param>
    ''' <param name="aMin">Radianti</param>
    ''' <param name="aMax">Radianti</param>
    ''' <returns>Coordinate polari</returns>
    Private Function CreaRandPolar(dMin As Double, dMax As Double, aMin As Single, aMax As Single) As PolarF
        'Angolo radianti
        Dim polR As PolarF
        Randomize()
        polR.L = dMin + Rnd() * (dMax - dMin)
        polR.A = aMin + Rnd() * (aMax - aMin)
        Return polR
    End Function

    ''' <summary>
    ''' Ottiene Punto in coordinate cartesiane da coordinate polari
    ''' </summary>
    ''' <param name="pP">Punto in coordinate polari</param>
    ''' <param name="pCentro">Punto centrale in coordinate cartesiane</param>
    ''' <returns></returns>
    Private Function CartFromPolar(pCentro As PointF, pP As PolarF) As PointF
        Dim x As Single = pCentro.X + Math.Cos(pP.A) * pP.L
        Dim y As Single = pCentro.Y + Math.Sin(pP.A) * pP.L
        Return New PointF(x, y)
    End Function

    ''' <summary>
    ''' Ruota Punto P dell'angolo Ang rispetto al punto pCentro
    ''' </summary>
    ''' <param name="cCentro">Coordinate Cartesiane</param>
    ''' <param name="P">Coordinate Polari</param>
    ''' <param name="Ang">Radianti</param>
    ''' <returns>Punto in coordinate Cartesiane</returns>
    Private Function Ruota(cCentro As PointF, P As PolarF, Ang As Single) As PointF
        Dim pR As PointF
        Dim L As Single = P.L
        Dim A As Single = P.A
        pR.X = cCentro.X + L * Math.Cos(Ang + A)
        pR.Y = cCentro.Y + L * Math.Sin(Ang + A)
        Return pR
    End Function
    ''' <summary>
    ''' Specchia il punto P rispetto al punto pCentro dell'angolo Ang
    ''' </summary>
    ''' <param name="cCentro">Coordinate Cartesiane</param>
    ''' <param name="P">Coordinate Polari</param>
    ''' <param name="Ang">Radianti</param>
    ''' <returns>Punto in coordinate Cartesiane</returns>
    Private Function Specchia(cCentro As PointF, P As PolarF, Ang As Single) As PointF
        Dim pR As PointF
        Dim L As Single = P.L
        Dim A As Single = P.A
        pR.X = cCentro.X + L * Math.Cos(Ang - A)
        pR.Y = cCentro.Y + L * Math.Sin(Ang - A)
        Return pR
    End Function

    ''' <summary>
    ''' Ottiene le coordinate Polari date le coordinate Cartesiane di un punto P rispetto al punto pCentro
    ''' </summary>
    ''' <param name="cCentro">coordinate Cartesiane</param>
    ''' <param name="P">coordinate Cartesiane</param>
    ''' <returns>coordinate Polari</returns>
    Private Function PolarFromCart(cCentro As PointF, P As PointF) As PolarF
        Dim pR As PolarF
        pR.L = Math.Sqrt((P.X - cCentro.X) ^ 2 + (P.Y - cCentro.Y) ^ 2)
        pR.A = Math.Atan2(P.Y - cCentro.Y, P.X - cCentro.X)
        Return pR
    End Function

    ''' <summary>
    ''' Ritorna una linea da 2 punti PolarF (2punti cartesiani) 
    ''' Elaborata con parametri Moltiplicazione,Posizione e Angolo
    ''' </summary>
    ''' <param name="cCentro">Punto cartesiano del Centro</param>
    ''' <param name="pP1">Punto 1 Polare</param>
    ''' <param name="pP2">Punto 2 Polare</param>
    ''' <returns>Linea in coordinate Cartesiane</returns>
    Private Function CreaLineaGradiente(cCentro As PointF, pP1 As PolarF, pP2 As PolarF, R As Integer) As LineF
        Dim vm As Single = HSB_BrusMol.Value / 10
        Dim vp As Single = HSB_BrusPos.Value / 10
        Dim va As Single = HSB_BrusAng.Value / 10 * Math.PI * R
        Dim pC1 As PointF = CartFromPolar(cCentro, pP1)
        Dim pC2 As PointF = CartFromPolar(cCentro, pP2)
        If pC1 = pC2 Then pC1.X += 1
        'Punto Medio Cartesiano
        Dim pMc As PointF
        pMc.X = pC1.X + (pC2.X - pC1.X) / 2
        pMc.Y = pC1.Y + (pC2.Y - pC1.Y) / 2
        Dim LR As New LineF
        LR.P1.X = pMc.X + (pC1.X - pMc.X) * vm + (pC1.X - pMc.X) * vp
        LR.P1.Y = pMc.Y + (pC1.Y - pMc.Y) * vm + (pC1.Y - pMc.Y) * vp
        LR.P2.X = pMc.X + (pC2.X - pMc.X) * vm + (pC1.X - pMc.X) * vp
        LR.P2.Y = pMc.Y + (pC2.Y - pMc.Y) * vm + (pC1.Y - pMc.Y) * vp
        LR.P1 = RuotaP1attP0diAng(LR.P1, pMc, va)
        LR.P2 = RuotaP1attP0diAng(LR.P2, pMc, va)
        Return LR
    End Function

    ''' <summary>
    ''' Ruota il punto P1 attorno al punto P0 dell'angolo Ang
    ''' </summary>
    ''' <param name="P1">coordinate Cartesiane</param>
    ''' <param name="P0">coordinate Cartesiane</param>
    ''' <param name="Ang">Radianti</param>
    ''' <returns>coordinate Cartesiane</returns>
    Private Function RuotaP1attP0diAng(P1 As PointF, P0 As PointF, Ang As Single) As PointF
        Dim PR As PointF
        PR.X = (P1.X - P0.X) * Math.Cos(Ang) - (P1.Y - P0.Y) * Math.Sin(Ang) + P0.X
        PR.Y = (P1.X - P0.X) * Math.Sin(Ang) + (P1.Y - P0.Y) * Math.Cos(Ang) + P0.Y
        Return PR
    End Function

#End Region

#Region "Salva e Carica File"
    Private Sub B_Salva_Click(sender As Object, e As EventArgs) Handles B_Salva.Click
        Dim saveFileD As New SaveFileDialog()
        Dim bmpSalva As New Bitmap(PB.Image)
        bmpSalva.MakeTransparent(ColorTranslator.FromOle(&HFFFFFF))  ' Rende trasparente il colore bianco
        Dim g As Graphics = Graphics.FromImage(bmpSalva)

        ' Se lo sfondo non è trasparente aggiunge lo sfondo
        If Not (CB_SfondoTrasp.Checked) Then
            g.DrawImage(PB.BackgroundImage, 0, 0)
            g.DrawImage(PB.Image, 0, 0)
        End If

        saveFileD.FileName = ("Mandala.png")
        saveFileD.Filter = "png files (*.png)|*.png|All files (*.*)|*.*"
        saveFileD.FilterIndex = 1

        If saveFileD.ShowDialog() = DialogResult.OK Then
            bmpSalva.Save(saveFileD.FileName, System.Drawing.Imaging.ImageFormat.Png)
        End If
        g.Dispose()
    End Sub

    Private Sub B_Carica_Click(sender As Object, e As EventArgs) Handles B_Carica.Click
        Dim openFileD As New OpenFileDialog With {
            .FileName = ("*.png"), .Filter = "png files (*.png)|*.png"}
        If openFileD.ShowDialog() = DialogResult.OK Then
            PB.Image = Image.FromFile(openFileD.FileName)
        End If
    End Sub

#End Region

End Class