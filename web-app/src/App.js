import { useEffect } from 'react';
import './App.css';
import Unity, { UnityContext } from "react-unity-webgl";

const unityContext = new UnityContext({
  loaderUrl: './Game/Build/build.loader.js',
  dataUrl: "./Game/Build/build.data",
  frameworkUrl: "./Game/Build/build.framework.js",
  codeUrl: "./Game/Build/build.wasm",
});

function App() {

  useEffect(function () {
    unityContext.on("canvas", function (canvas) {
      canvas.width = 1050;
      canvas.height = 600;
    });
  }, []);

  return (
    <div className="game-area">
      <Unity 
        unityContext={unityContext}
      />
    </div>
  );
}

export default App;
