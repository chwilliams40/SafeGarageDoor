using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;



namespace Solution
{
    public class Door
    {
        private int _doorPosition = 0;
        private bool _opening = true; // true = Opening, false = closing
        private bool _moving = false;  // true = moving, false = stopped

        public string ProcessEvents(string events)
        {
            //string doorCode = "";
            string output = "";

            for (int i = 0; i < events.Length; i++)
            {
                switch (events[i])
                {
                    case 'P':
                        _moving = !_moving;
                        if (_moving)
                        {
                            UpdateDoorPosition();
                            CheckLimits();
                        }
                        break;
                    case 'O':
                        if (_moving)
                        {
                            _opening = !_opening;
                            UpdateDoorPosition();
                            CheckLimits();
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid code: {events}");
                        }
                        break;
                    case '.':
                        if (_moving)
                        {
                            UpdateDoorPosition();
                            CheckLimits();
                        }
                        break;
                }

                output += _doorPosition;
            }

            return output;
        }

        private void CheckLimits()
        {
            if (_doorPosition == 5 || _doorPosition == 0)
            {
                _moving = false;
                _opening = !_opening;
            }
        }

        private void UpdateDoorPosition()
        {
            if (_opening)
            {
                _doorPosition++;
            }
            else
            {
                _doorPosition--;
            }
        }
    }
}


