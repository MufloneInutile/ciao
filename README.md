# ciao — Movimento personaggio in Unity (C#)

Script per muovere un personaggio in Unity con camminata, corsa, salto, gravità
e rotazione della telecamera con il mouse.

## File

- `Assets/Scripts/PlayerMovement.cs` — movimento WASD, corsa (Shift), salto (Spazio), gravità.
- `Assets/Scripts/MouseLook.cs` — rotazione vista in prima persona con il mouse.

## Setup in Unity

1. **Crea il personaggio**
   - Nella scena, crea un GameObject (es. una Capsule): `GameObject > 3D Object > Capsule`.
   - Aggiungi il componente **Character Controller**: `Add Component > Character Controller`.
   - Aggiungi lo script **PlayerMovement** allo stesso GameObject.

2. **Controllo del terreno (consigliato)**
   - Crea un GameObject vuoto come figlio del personaggio, posizionalo ai suoi piedi
     e chiamalo `GroundCheck`.
   - Trascinalo nel campo **Ground Check** dello script.
   - Imposta il campo **Ground Mask** sul layer del terreno (es. crea un layer `Ground`
     e assegnalo al pavimento).

3. **Telecamera**
   - Rendi la **Main Camera** figlia del personaggio e posizionala all'altezza degli occhi.
   - Aggiungi lo script **MouseLook** alla camera.
   - Trascina il GameObject del personaggio nel campo **Player Body** dello script.

4. **Premi Play**
   - `W A S D` per muoverti
   - `Shift` per correre
   - `Spazio` per saltare
   - Muovi il **mouse** per guardarti intorno

## Parametri regolabili (nell'Inspector)

| Parametro | Descrizione |
|-----------|-------------|
| Move Speed | Velocità di camminata |
| Run Speed | Velocità di corsa |
| Jump Height | Altezza del salto |
| Gravity | Forza di gravità (valore negativo) |
| Mouse Sensitivity | Sensibilità del mouse |
