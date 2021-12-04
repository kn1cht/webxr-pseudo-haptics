---
layout: page
title: About
---

## Pseudo-haptics



<style>
</style>

<div class="flex-container">
  <section class="col">
    <p>
      Pseudo-haptics is a type of haptic illusion caused by the gap between the users' input and the visual stimulus.
      For example, the user feels some illusory resistance force if the mouse cursor moves slower than usual.
      Pseudo-haptics can make users perceive such as weight, friction, roughness without mechanical haptics devices.
    </p>
    <p>
      WebXR Pseudo-haptics is a set of browser-based demonstrations of pseudo-haptics.
      In addition to experiencing pseudo-haptics on a 2D monitor, the users can also experience it in a virtual environment using head-mounted displays.
    </p>
  </section>
  <section class="col">
    <p>
      Pseudo-haptics（疑似触覚）は，ユーザの入力と視覚的な表示のずれによって生起する触覚の錯覚です。
      例えば、マウスカーソルの動きが普段より遅いと、抵抗力が大きくなったかのように感じる現象はpseudo-hapticsの一種です。
      物理的な触覚提示デバイスがなくても、ユーザに重さや摩擦、凹凸などを知覚させることができます。
    </p>
    <p>
      WebXR Pseudo-hapticsは、ブラウザで動作するpseudo-hapticsのデモンストレーションです。
      2Dモニタでの体験に加えて、ヘッドマウントディスプレイを用いてバーチャル空間でもpseudo-hapticsを体験できます。
    </p>
  </section>
</div>

## How to experience
### PCs
Drag objects in the scenes or move the cursor over them.

画面中の物を掴んで動かしたり、カーソルでなぞったりしてください。

<div class="flex-container">
  <img src="https://i.gyazo.com/dffaa58cada19a5fb0ba25b322996be2.gif" alt="demonstration of pseudo-haptics (compliance)" class="col" />
  <img src="https://i.gyazo.com/71b1b2c9f33745b9c39739b6481b360a.gif" alt="demonstration of pseudo-haptics (macro roughness)" class="col" />
</div>

\* Although the demonstraions work with touchscreens, the effect of pseudo-haptics may be reduced because the mismatch between your actual finger and the visual stimuli is visible.

※デモはタッチパネルでも体験できますが、実際の指と視覚刺激の不一致が見えてしまうため、pseudo-hapticsの効果が弱まる可能性があります。

### VR headsets (standalone)
[Firefox Reality](https://mixedreality.mozilla.org/firefox-reality/) browser or Oculus Browser are recommended.
Click "VR" buttons at the right bottom of the scene.
You may be asked to allow WebXR on kn1cht.github.io.

体験するWebブラウザは、[Firefox Reality](https://mixedreality.mozilla.org/firefox-reality/)またはOculus Browserをお勧めします。
画面の右下にある「VR」ボタンをクリックするとVRモードに移行します。
kn1cht.github.ioでWebXRの実行を許可するように求められた場合は、許可してください。

<div class="flex-container">
  <video class="col" src="{{site.baseurl}}/public/videos/webxrph-oculus-browser.mp4" autoplay muted loop playsinline controls>
    <p>Your browser doesn't support HTML5 video. Here is a <a href="{{site.baseurl}}/public/videos/webxrph-oculus-browser.mp4">link to the video</a> instead.</p>
  </video>
</div>

### VR headsets (PC)
Use [the browsers which support WebXR Device API](https://caniuse.com/webxr) (e.g. Chrome, Edge).
Make sure that you connect the headset to the PC and click "VR" buttons at the right bottom of the scene.
You may be asked to allow WebXR on kn1cht.github.io.

体験するWebブラウザは、[WebXR Device APIをサポートしているもの](https://caniuse.com/webxr)（例：Chrome, Edge）をお勧めします。
ヘッドセットがPCに接続できていることをご確認の上、画面の右下にある「VR」ボタンをクリックするとVRモードに移行します。
kn1cht.github.ioでWebXRの実行を許可するように求められた場合は、許可してください。

<div class="flex-container">
  <video class="col" src="{{site.baseurl}}/public/videos/webxrph-pc-chrome.mp4" autoplay muted loop playsinline controls>
    <p>Your browser doesn't support HTML5 video. Here is a <a href="{{site.baseurl}}/public/videos/webxrph-pc-chrome.mp4">link to the video</a> instead.</p>
  </video>
</div>




