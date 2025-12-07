{
  description = "dotnet monogame flake";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
  };

  outputs = { self, nixpkgs }:
  let
    system = "x86_64-linux";
    pkgs = nixpkgs.legacyPackages.${system};
  in
  {
    devShells.${system}.default = pkgs.mkShell {
      buildInputs = with pkgs; [
        dotnet-sdk_9
      ];
      LD_LIBRARY_PATH = pkgs.lib.makeLibraryPath [
          pkgs.freetype
          pkgs.libGL
          pkgs.pulseaudio
          pkgs.xorg.libX11
          pkgs.xorg.libXrandr
      ];
    };
  };
}
