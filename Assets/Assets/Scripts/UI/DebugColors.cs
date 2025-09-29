using UnityEngine;


// Este script debe mandarse a llamar desde dode quieras poner debug con el using TGDebugColors;
// y se hace un print de color asi : DebugColors.printColor("Mensaje del debug",DebugColors.ORANGE);, solo que donde va el color pones el color deseado.
// o asi si quieres el color verde : DebugColors.printSuccess("Mensaje Verde de Success");
// o asi si quieres el color rojo  : DebugColors.printFailed("Mensaje Rojo de Failed");

namespace TGDebugColors
{
    /// <summary>
    /// Utilidad para imprimir mensajes con color en consola, usando etiquetas <color>.
    /// </summary>
    public static class DebugColors
    {

        #region Colores_Disponibles
        //  Colores disponibles
        public const string RED = "<color=red>";
        public const string GREEN = "<color=green>";
        public const string BLUE = "<color=blue>";
        public const string YELLOW = "<color=yellow>";
        public const string CYAN = "<color=cyan>";
        public const string MAGENTA = "<color=magenta>";
        public const string WHITE = "<color=white>";
        public const string GRAY = "<color=gray>";
        public const string BLACK = "<color=black>";
        public const string ORANGE = "<color=#FFA500>";
        public const string GOLD = "<color=#FFD700>";
        public const string PURPLE = "<color=#800080>";
        #endregion

        /// <summary> Imprime un mensaje con color usando Debug.Log. </summary>
        public static void printColor(string message, string color)
        {
            Debug.Log($"{color}{message}</color>");
        }

        /// <summary> Imprime un mensaje con color verde </summary>
        public static void printSuccess(string message)
        {
            Debug.Log($"{GREEN}{message}</color>");
        }

        /// <summary> Imprime un mensaje con color rojo </summary>
        public static void printFailed(string message)
        {
            Debug.Log($"{RED}{message}</color>");
        }


        /// <summary> Imprime un mensaje con color como advertencia usando Debug.LogWarning. </summary>
        public static void printLogWarning(string message)
        {
            Debug.LogWarning($"{YELLOW}{message}</color>");
        }

        /// <summary> Imprime un mensaje con color como error usando Debug.LogError. </summary>
        public static void printLogError(string message)
        {
            Debug.LogError($"{CYAN}{message}</color>");
        }


    }
}
