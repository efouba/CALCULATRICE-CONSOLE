' ============================================================
'  PROJET 1 — Calculatrice Console Interactive
'  VB.NET / Framework 4.6
'  Compétences : I/O console, types, boucles, Try/Catch, Sub/Function
' ============================================================

Module Module1

    ' Tableau circulaire pour l'historique (5 entrées max)
    Dim historique(4) As String
    Dim indexHisto As Integer = 0
    Dim nbEntrees As Integer = 0

    ' ---- Point d'entrée ----------------------------------------
    Sub Main()
        Console.Title = "Calculatrice Console — Projet 1"
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Dim continuer As Boolean = True

        AfficherBanniere()

        Do While continuer
            AfficherMenu()
            Dim choix As String = Console.ReadLine().Trim()

            Select Case choix
                Case "1" : TraiterOperation("+")
                Case "2" : TraiterOperation("-")
                Case "3" : TraiterOperation("*")
                Case "4" : TraiterOperation("/")
                Case "5" : TraiterOperation("^")
                Case "6" : TraiterRacineCarree()
                Case "7" : TraiterOperation("Mod")
                Case "8" : AfficherHistorique()
                Case "9" : continuer = False
                Case Else
                    AfficherErreur("Choix invalide. Tapez un chiffre entre 1 et 9.")
            End Select
        Loop

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine($"{vbCrLf}  Au revoir ! {nbEntrees} calcul(s) effectué(s).")
        Console.ResetColor()
        Console.ReadKey(True)
    End Sub

    ' ---- Affichage bannière ------------------------------------
    Sub AfficherBanniere()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("╔══════════════════════════════════════╗")
        Console.WriteLine("║   CALCULATRICE CONSOLE  — VB.NET     ║")
        Console.WriteLine("║         Projet 1 / Framework 4.6     ║")
        Console.WriteLine("╚══════════════════════════════════════╝")
        Console.ResetColor()
    End Sub

    ' ---- Menu principal ----------------------------------------
    Sub AfficherMenu()
        Console.WriteLine()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("  [1] Addition        [2] Soustraction")
        Console.WriteLine("  [3] Multiplication  [4] Division")
        Console.WriteLine("  [5] Puissance       [6] Racine carrée")
        Console.WriteLine("  [7] Modulo          [8] Historique")
        Console.WriteLine("  [9] Quitter")
        Console.ResetColor()
        Console.Write("  Votre choix : ")
    End Sub

    ' ---- Traitement opérations binaires -----------------------
    Sub TraiterOperation(ByVal op As String)
        Dim a As Double, b As Double

        If Not LireDouble("  Entrez le premier nombre  : ", a) Then Return
        If Not LireDouble("  Entrez le deuxième nombre : ", b) Then Return

        Dim resultat As Double
        Dim expression As String

        Try
            Select Case op
                Case "+"
                    resultat = a + b
                    expression = $"{a} + {b} = {resultat}"
                Case "-"
                    resultat = a - b
                    expression = $"{a} - {b} = {resultat}"
                Case "*"
                    resultat = a * b
                    expression = $"{a} × {b} = {resultat}"
                Case "/"
                    If b = 0 Then
                        AfficherErreur("Division par zéro impossible !")
                        Return
                    End If
                    resultat = a / b
                    expression = $"{a} ÷ {b} = {resultat:F6}"
                Case "^"
                    resultat = Math.Pow(a, b)
                    expression = $"{a} ^ {b} = {resultat}"
                Case "Mod"
                    If b = 0 Then
                        AfficherErreur("Modulo par zéro impossible !")
                        Return
                    End If
                    resultat = a Mod b
                    expression = $"{a} Mod {b} = {resultat}"
            End Select

            AfficherResultat(expression)
            EnregistrerHistorique(expression)

        Catch ex As OverflowException
            AfficherErreur("Dépassement de capacité numérique.")
        Catch ex As Exception
            AfficherErreur($"Erreur inattendue : {ex.Message}")
        End Try
    End Sub

    ' ---- Racine carrée ----------------------------------------
    Sub TraiterRacineCarree()
        Dim a As Double
        If Not LireDouble("  Entrez le nombre : ", a) Then Return

        If a < 0 Then
            AfficherErreur("Impossible : racine d'un nombre négatif.")
            Return
        End If

        Dim resultat As Double = Math.Sqrt(a)
        Dim expression As String = $"√{a} = {resultat:F6}"
        AfficherResultat(expression)
        EnregistrerHistorique(expression)
    End Sub

    ' ---- Lecture sécurisée d'un Double ------------------------
    Function LireDouble(ByVal invite As String, ByRef valeur As Double) As Boolean
        Console.Write(invite)
        Dim saisie As String = Console.ReadLine()

        If Double.TryParse(saisie, _
               Globalization.NumberStyles.Any, _
               Globalization.CultureInfo.InvariantCulture, _
               valeur) Then
            Return True
        Else
            AfficherErreur($"« {saisie} » n'est pas un nombre valide.")
            Return False
        End If
    End Function

    ' ---- Affichage résultat -----------------------------------
    Sub AfficherResultat(ByVal expr As String)
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine($"{vbCrLf}  ✔  Résultat : {expr}{vbCrLf}")
        Console.ResetColor()
        nbEntrees += 1
    End Sub

    ' ---- Affichage erreur -------------------------------------
    Sub AfficherErreur(ByVal msg As String)
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine($"{vbCrLf}  ✘  Erreur : {msg}{vbCrLf}")
        Console.ResetColor()
    End Sub

    ' ---- Enregistrement historique (tableau circulaire) -------
    Sub EnregistrerHistorique(ByVal expr As String)
        historique(indexHisto Mod 5) = expr
        indexHisto += 1
    End Sub

    ' ---- Affichage historique ---------------------------------
    Sub AfficherHistorique()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine($"{vbCrLf}  ── Historique des 5 derniers calculs ──")
        Console.ResetColor()

        Dim aucun As Boolean = True
        For i As Integer = 0 To 4
            If historique(i) IsNot Nothing AndAlso historique(i) <> "" Then
                Console.WriteLine($"  [{i + 1}] {historique(i)}")
                aucun = False
            End If
        Next

        If aucun Then
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("  (aucun calcul pour le moment)")
            Console.ResetColor()
        End If
        Console.WriteLine()
    End Sub

End Module

===Efouba Kosso
