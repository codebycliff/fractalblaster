(dir ..\src\FractalBlaster -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Core -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Family -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.AudioOut -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.AvailablePluginsView -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.ChopperEffect -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.ColorVisualizer -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.Decoder -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.M3UPlaylist -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.WaveView -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Plugins.XSPFPlaylist -include *.cs, *.h, *.cpp -recurse | select-string .).Count + 
(dir ..\src\FractalBlaster.Universe -include *.cs, *.h, *.cpp -recurse | select-string .).Count