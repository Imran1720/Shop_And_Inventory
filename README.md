# Shop & Inventory System
After diving deep into key design patterns at Outscal (Singleton, Service Locator, MVC, and Observer), I was given a challenge:
🎯 Build a complete UI-based Shop & Inventory mechanic from the ground up — to showcase my understanding and ability to apply these patterns meaningfully in a game architecture.

Here’s what I built 👇

### 🛒 SHOP SYSTEM :
<ol>
<li>Items categorized by type: <b>Weapon</b>, <b>Treasure</b>, <b>Material</b>, <b>Consumable</b>, or <b>All</b></li>
<li>Items come with rarity (Very Common ➡️ Legendary), each with a unique visual identity</li>
<li>Player can sell items to the shop, which get added dynamically</li>
<li><b>Timer</b> resets shop inventory (including sold items) after cooldown</li>
<li>Item filters, scalable UI, and item highlight system</li>
</ol>

### 🎒 INVENTORY SYSTEM :
<ol>
<li><b>100 kg weight cap</b> – can’t gather or buy beyond it</li>
<li>Items are gathered <b>randomly (1-3 at a time)</b> from a ScriptableObject-driven database</li>
<li><b>Buying/selling</b> affects coin balance and inventory weight in real-time</li>
<li>UI dynamically updates selected item in an ItemCard with detailed info and contextual buy/sell option</li>
</ol>

## 🎯 How I Applied Design Patterns:
<ol>
    <li><b>Singleton:</b> For services like <b>EventService</b> and <b>GameService</b> while prototyping — making event dispatching and shared data access universal and clean</li>
  <li><b>Service Locator:</b> To decouple dependencies and share services like <b>SoundManager</b>, <b>EventService</b> and <b>GameService</b></li>
  <li><b>MVC:</b> Used extensively in <b>Shop UI</b>, <b>Inventory UI</b>, and <b>ItemCard system</b> to separate logic, visuals, and behavior for modular, scalable design</li>
  <li><b>Observer:</b> Powers the <b>pop-up notification system (bought item, insufficient coins, full inventory)</b> and updates on item selection</li>
</ol>

## 🧠 What I Learned:
Initially, I rushed to get a working prototype. But the real challenge came during refactoring — implementing design patterns after-the-fact is tough. This experience taught me the value of architectural planning early on.

I now feel more confident using MVC and related patterns not just in theory or following tutorials, but by building everything from scratch.
