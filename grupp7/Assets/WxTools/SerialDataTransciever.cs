using UnityEngine;

namespace WxTools.IO
{
    /// <summary>
    /// SerialDataTransciever v1.0.6
    /// </summary>
    public abstract class SerialDataTransciever : MonoBehaviour
    {
        /// <summary>
        /// The connected SerialCommunication object
        /// </summary>
        [SerializeField]
        protected SerialCommunication communicator;

        /// <summary>
        /// The data prefix that this SerialDataTransciever object is listening for
        /// If no data prefix is defined, that raw data will be sent to be parsed
        /// </summary>
        [SerializeField]
        protected string dataReadPrefix = "";

        void Awake()
        {
            if (communicator != null)
                communicator.DataRecieved += ArduinoSerialCommunication_DataRecieved;
        }

        private void ArduinoSerialCommunication_DataRecieved(object sender, string data)
        {

            // no data
            if (data.Length <= 0)
                return;

            // prefix defined
            if(dataReadPrefix != string.Empty) // prefix defined listen to just that prefix
            {
                //data = "#B1000"

                // data starting with the defined prefix, otherwised discard 
                if (data.StartsWith(dataReadPrefix))
                {
                    
                    // remove prefix from data and send it to be parsed
                    data = data.Remove(0, dataReadPrefix.Length);

                    //data = "1000"

                    ParseData(data);

                    float maximumValue = 1000.0f;
                    if (communicator != null)
                        maximumValue = (float)communicator.arduinoMaximumOutputValue;
                    
                    int value = 0;
                    if (int.TryParse(data, out value) == true)
                    {
                        float ratio = value / maximumValue;
                        ratio = Mathf.Clamp01(ratio);
                        RecieveDataAsRatio01(ratio);
                    }
                    else
                        Debug.LogWarning("TryParse int from data " + data + " failed!");
                }
            }
            // no prefix defined, send raw data to be parsed
            else
            {
               // data = data.Substring(2, data.Length - 2); // hack: remove the two starting chars
                ParseData(data);
            }
        }

        /// <summary>
        /// Send data via the SerialCommunication object
        /// </summary>
        /// <param name="data">The data to send</param>
        protected virtual void SendData(string data)
        {
            if (communicator != null)
                communicator.SendData(this, data);
        }

        /// <summary>
        /// Override this method to be able to parse and react to recieved data
        /// </summary>
        /// <param name="data">The data to parse</param>
        protected virtual void ParseData(string data)
        {
        }

        /// <summary>
        /// Override this method to be able to parse and react to recieved data
        /// </summary>
        /// <param name="ratio">The ratio ranging from 0 to 1.0f</param>
        protected virtual void RecieveDataAsRatio01(float ratio)
        {
        }

        /// <summary>
        /// Draw gizmos when selected in the Unity Editor
        /// </summary>
        public void OnDrawGizmosSelected()
        {
            if (communicator != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(this.transform.position, communicator.transform.position);
            }
        }
    }
}
