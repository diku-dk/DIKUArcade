{ pkgs ? import <nixpkgs> {} }:

with pkgs;
mkShell {
  nativeBuildInputs =
    with buildPackages; [
      (with dotnetCorePackages; combinePackages [
        sdk_6_0
        sdk_8_0
      ])
      fsautocomplete
      stdenv
      libglvnd
    ] ++ (with xorg; [ libX11 libXext libXinerama libXi libXrandr ]);
  
  LD_LIBRARY_PATH =
    with xorg; "${libX11}/lib:${libXext}/lib:${libXinerama}/lib:${libXi}/lib:${libXrandr}/lib:${libglvnd}/lib";
}
